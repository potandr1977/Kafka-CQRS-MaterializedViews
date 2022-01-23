using EventBus.Kafka.Abstraction.Abstraction;
using Messages;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Kafka.Abstraction
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddKafkaProducer<TMessage>(
            this IServiceCollection serviceCollection,
            string TopicName) where TMessage : UpdateProjectionMessage
        {
            var producer = new KafkaProducer<TMessage>(TopicName);
            serviceCollection.AddSingleton<IKafkaProducer<TMessage>>(producer);

            return serviceCollection;
        }

        public static IServiceCollection AddKafkaConsumer<TMessage, TMessageHandler>(
            this IServiceCollection serviceCollection,
            string TopicName,
            string GroupdId) where TMessage : UpdateProjectionMessage where TMessageHandler : class, IMessageHandler<TMessage>
        {
            serviceCollection.AddSingleton<IMessageHandler<TMessage>, TMessageHandler>();

            var consumer = new KafkaConsumer<string, TMessage>(
                GroupdId,
                TopicName,
                 (key,value) => {
                    var messageHandler = serviceCollection.BuildServiceProvider().GetRequiredService<IMessageHandler<TMessage>>();

                    //will be executed synchronously
                    messageHandler.HandleAsync(value);
                });

            serviceCollection.AddSingleton<IKafkaConsumer<TMessage>>(consumer);

            return serviceCollection;
        }

    }
}
