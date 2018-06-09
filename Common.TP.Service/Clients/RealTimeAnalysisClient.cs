using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Common.Entities;

namespace Common.TP.Service
{
    public class RealTimeAnalysisClient : IRealTimeAnalysisClient
    {
        private const string ExchangeId = BrokerTopic.AnalysisRealtime;
        private const string StockRoute = BrokerRoute.AllAnalysis;
        private IModel m_Channel;
        public Action<IList<TransactionAnalysis>> OnReceiveTransactionAnalysis { get; set; }

        public bool Init()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            m_Channel = connection.CreateModel();

            m_Channel.ExchangeDeclare(exchange: ExchangeId, type: "topic");

            var queueName = m_Channel.QueueDeclare().QueueName;

            m_Channel.QueueBind(queue: queueName,
                                      exchange: ExchangeId,
                                      routingKey: StockRoute);

            var consumer = new EventingBasicConsumer(m_Channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var routingKey = ea.RoutingKey;
                var keys = routingKey.Split('.');
                if (keys.Length >= 3)
                {
                    string root = keys[0];
                    string exchange = keys[1];
                    string dataType = keys[2];

                    if ("analysis".Equals(root, StringComparison.OrdinalIgnoreCase))
                    {
                        if ("transactions".Equals(dataType, StringComparison.OrdinalIgnoreCase))
                        {
                            BrokerMessage<IList<TransactionAnalysis>> message = null;

                            message = body.Deserialize<BrokerMessage<IList<TransactionAnalysis>>>();

                            if (message != null)
                                OnReceiveTransactionAnalysis?.Invoke(message.Data);

                        }
                        else
                        {
                            //unknow message
                        }
                    }
                }
            };

            m_Channel.BasicConsume(queue: queueName,
                     noAck: true,
                     consumer: consumer);


            return true;
        }
    }
}
