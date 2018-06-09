using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Common.TP.Service.Services
{
    public class BaseService
    {
        public BaseService(IConnection connection, string brokerQueueId)
        {
            Type type = GetType();
            string className = type.Name;

            //get public methods
            var methodNames = type.GetMethods().Where(r => r.IsFinal && !r.IsSpecialName).ToList();

            foreach (MethodInfo method in methodNames)
            {
                var isReturnVoidMethod = (method.ReturnType.Name == "Void");
                var hasInputParameter = method.GetParameters().Count() > 0;

                IModel channel = connection.CreateModel();
                string queueName = $"{brokerQueueId}.{className}.{method.Name}";

                channel.QueueDeclare(queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                var consumer = new EventingBasicConsumer(channel);

                channel.BasicConsume(queue: queueName,
                 noAck: false,
                 consumer: consumer);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var requests = body?.Deserialize<object[]>();
                    requests = requests ?? new object[0];

                    var props = ea.BasicProperties;
                    if (!string.IsNullOrEmpty(props.ReplyTo) && !string.IsNullOrEmpty(props.CorrelationId))
                    {
                        object response = null;
                        var replyProps = channel.CreateBasicProperties();
                        replyProps.CorrelationId = props.CorrelationId;

                        try
                        {
                            if (method.ReturnType.Namespace == "System.Threading.Tasks")
                            {
                                var task = (Task)method.Invoke(this, requests);
                                task.Wait();
                                response = task.GetType().GetProperty("Result").GetValue(task, null);
                            }
                            else
                            {
                                response = method.Invoke(this, requests);
                            }
                        }
                        catch (Exception ex)
                        {
                            //log ex
                            Console.WriteLine(ex.ToString());
                        }
                        finally
                        {
                            try
                            {
                                byte[] responseBytes = response == null? new byte[0] : response.SerializeToByteArray();
                                channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                                  basicProperties: replyProps, body: responseBytes);
                                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                            }
                            catch (Exception ex)
                            {
                                //log ex
                                Console.WriteLine(ex.ToString());
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            method.Invoke(this, requests);
                        }
                        catch (Exception ex)
                        {
                            //log ex
                            Console.WriteLine(ex.ToString());
                        }
                    }
                };
            }
        }
    }
}
