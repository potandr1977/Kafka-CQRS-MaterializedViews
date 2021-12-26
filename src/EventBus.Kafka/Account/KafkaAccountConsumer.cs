using EventBus.Kafka.Abstraction;
using EventBus.Kafka.Abstraction.Messages;
using Settings;

namespace EventBus.Kafka
{
    /// <summary>  
    /// Base class for implementing Kafka Consumer.  
    /// </summary>  
    /// <typeparam name="TKey"></typeparam>  
    /// <typeparam name="TValue"></typeparam>  
    public class KafkaAccountConsumer : KafkaConsumer<string, UpdateAccountProjectionMessage>, IKafkaAccountConsumer
    {
        /// <summary>  
        /// Indicates constructor to initialize the serviceScopeFactory and ConsumerConfig  
        /// </summary>  
        /// <param name="config">Indicates the consumer configuration</param>  
        /// <param name="serviceScopeFactory">Indicates the instance for serviceScopeFactory</param>  
        public KafkaAccountConsumer():base(KafkaSettings.BusinessGroupId,KafkaSettings.AccountTopicName)
        {
        }
    }
}
