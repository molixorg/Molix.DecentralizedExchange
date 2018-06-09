using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Threading.Tasks;

namespace Common.TP.Service.Clients
{
    public class BaseClient
    {
        private IConnection m_Connection;
        private string m_BrokerQueueId;
        private string m_ClassName;
        private int m_TimeOut;
        public BaseClient(string brokerQueueId)
        {
            m_BrokerQueueId = brokerQueueId;
            m_Connection = new ConnectionFactory().CreateConnection();
            m_ClassName = GetType().Name;
            m_TimeOut = 10000;
        }
        protected T Proxy<T>(string methodName, params object[] parameters)
        {
            using (var channel = m_Connection.CreateModel())
            {
                string rpcQueueName = $"{m_BrokerQueueId}.{m_ClassName}.{methodName}";

                channel.QueueDeclare(queue: rpcQueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

                SimpleRpcClient client = new SimpleRpcClient(channel, "", "direct", rpcQueueName);
                client.TimeoutMilliseconds = m_TimeOut;

                var corrId = Guid.NewGuid().ToString();
                IBasicProperties props = channel.CreateBasicProperties();
                props.Persistent = false;
                props.ReplyTo = channel.QueueDeclare(queue : $"rpc.{rpcQueueName}.{corrId}").QueueName;
                props.CorrelationId = corrId;

                parameters = parameters ?? new object[0];
                byte[] messageBytes = parameters.SerializeToByteArray();

                var response = client.Call(props, messageBytes);

                if (response == null)
                {
                    return default(T);
                }

                byte[] replyMessageBytes = response.Body;

                if (replyMessageBytes == null || replyMessageBytes.Length <= 0)
                {
                    return default(T);
                }

                try
                {
                    T result = replyMessageBytes.Deserialize<T>();
                    return result;
                }
                catch (Exception ex)
                {
                    //TODO : log ex
                }

                return default(T);
            }
        }

        protected void Proxy(string methodName, params object[] parameters)
        {
            using (var channel = m_Connection.CreateModel())
            {
                string queueName = $"{m_BrokerQueueId}.{m_ClassName}.{methodName}";

                parameters = parameters ?? new object[0];
                byte[] messageBytes = parameters.SerializeToByteArray();

                var properties = channel.CreateBasicProperties();
                properties.Persistent = false;

                channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: properties,
                                 body: messageBytes);
            }
        }

        protected Task<T> ProxyAsync<T>(string methodName, params object[] parameters)
        {
            return Task<T>.Factory.StartNew(() => Proxy<T>(methodName, parameters));
        }

        protected Task ProxyAsync(string methodName, params object[] parameters)
        {
            return Task.Factory.StartNew(() => Proxy(methodName, parameters));
        }
    }
}
