using Messages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Kafka.Abstraction.Extension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddKafkaProducer<TKey, TValue>(
            this IServiceCollection serviceCollection,
            string TopicName) where TValue : UpdateProjectionMessage
        {
            var producer = new KafkaProducer<TKey, TValue>(TopicName);
            serviceCollection.AddSingleton<IKafkaProducer<TKey,TValue>>(producer);

            return serviceCollection;
        }
        public static IServiceCollection AddKafkaConsumer<TKey, TValue,TMessageHandler>(
            this IServiceCollection serviceCollection,
            string TopicName,
            string GroupdId) where TValue : UpdateProjectionMessage where TMessageHandler : IMessageHandler<TValue>, new()
        {
            var consumer = new KafkaConsumer<TKey, TValue>(
                GroupdId,
                TopicName,
                async (key,value) => {
                    var messageHandler = new TMessageHandler();
                    await messageHandler.HandleAsync(value);
                });

            serviceCollection.AddSingleton<IKafkaConsumer<TKey, TValue>>(consumer);

            return serviceCollection;
        }

    }
}
