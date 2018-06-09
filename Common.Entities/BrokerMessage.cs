using System;

namespace Common.Entities
{
    [Serializable]
    public class BrokerMessage<T>
    {
        public DateTime SentDate { get; set; }
        public T Data { get; set; }
        public BrokerMessage(T data)
        {
            SentDate = DateTime.UtcNow;
            Data = data;
        }
    }
}
