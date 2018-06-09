using Common.Domain;
using Common.Entities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.TP.Service
{
    public class RealTimeMarketDataClient : IRealTimeMarketDataClient
    {
        private string ExchangeId = BrokerTopic.MarketDataRealtime;
        private const string StockRoute = BrokerRoute.AllStocks;
        private const string ContentTypeJson = "application/json";

        private IModel m_Channel;

        public RealTimeMarketDataClient()
        {

        }

        public RealTimeMarketDataClient(string brokerExchangeId)
        {
            if (!string.IsNullOrEmpty(brokerExchangeId))
            {
                ExchangeId = brokerExchangeId;
            }
        }

        public Action<IList<Instrument>> OnChangeInstruments { get; set; }
        public Action<IList<PairOrder>> OnChangePairOrder { get; set; }
        public Action<IList<PairOrder>> OnChangeDeepPairOrder { get; set; }
        public Action<TransactionsInfo> OnChangeTransaction { get; set; }

        public bool Init(string queueName = null)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            m_Channel = connection.CreateModel();

            m_Channel.ExchangeDeclare(exchange: ExchangeId, type: "topic");

            var uniqueQueueName = string.Empty;

            if (string.IsNullOrEmpty(queueName))
            {
                uniqueQueueName = m_Channel.QueueDeclare().QueueName;
            }
            else
            {
                uniqueQueueName = m_Channel.QueueDeclare(queue: queueName).QueueName;
            }

            m_Channel.QueueBind(queue: uniqueQueueName,
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

                    if ("stock".Equals(root, StringComparison.OrdinalIgnoreCase))
                    {
                        if ("instruments".Equals(dataType, StringComparison.OrdinalIgnoreCase))
                        {
                            BrokerMessage<IList<Instrument>> message = null;

                            if (ea.BasicProperties != null && ContentTypeJson.Equals(ea.BasicProperties.ContentType))
                            {
                                string json = Encoding.ASCII.GetString(body);
                                message = JsonConvert.DeserializeObject<BrokerMessage<IList<Instrument>>>(json);
                            }
                            else
                            {
                                message = body.Deserialize<BrokerMessage<IList<Instrument>>>();
                            }

                            if (message != null)
                                OnChangeInstruments?.Invoke(message.Data);

                        }
                        else if ("orders".Equals(dataType, StringComparison.OrdinalIgnoreCase))
                        {
                            BrokerMessage<IList<PairOrder>> message = null;

                            if (ea.BasicProperties != null && ContentTypeJson.Equals(ea.BasicProperties.ContentType))
                            {
                                string json = Encoding.ASCII.GetString(body);
                                message = JsonConvert.DeserializeObject<BrokerMessage<IList<PairOrder>>>(json);
                            }
                            else
                            {
                                message = body.Deserialize<BrokerMessage<IList<PairOrder>>>();
                            }

                            if (message != null)
                                OnChangePairOrder?.Invoke(message.Data);
                        }
                        else if ("deepOrders".Equals(dataType, StringComparison.OrdinalIgnoreCase))
                        {
                            BrokerMessage<IList<PairOrder>> message = null;

                            if (ea.BasicProperties != null && ContentTypeJson.Equals(ea.BasicProperties.ContentType))
                            {
                                string json = Encoding.ASCII.GetString(body);
                                message = JsonConvert.DeserializeObject<BrokerMessage<IList<PairOrder>>>(json);
                            }
                            else
                            {
                                message = body.Deserialize<BrokerMessage<IList<PairOrder>>>();
                            }

                            if (message != null)
                                OnChangeDeepPairOrder?.Invoke(message.Data);
                        }
                        else if ("transactions".Equals(dataType, StringComparison.OrdinalIgnoreCase))
                        {
                            BrokerMessage<TransactionsInfo> message = null;

                            if (ea.BasicProperties != null && ContentTypeJson.Equals(ea.BasicProperties.ContentType))
                            {
                                string json = Encoding.ASCII.GetString(body);
                                message = JsonConvert.DeserializeObject<BrokerMessage<TransactionsInfo>>(json);
                            }
                            else
                            {
                                message = body.Deserialize<BrokerMessage<TransactionsInfo>>();
                            }

                            if (message != null)
                                OnChangeTransaction?.Invoke(message.Data);
                        }
                        else
                        {
                            //unknow message
                        }
                    }
                }
            };

            m_Channel.BasicConsume(queue: uniqueQueueName,
                     noAck: true,
                     consumer: consumer);


            return true;
        }
    }
}
