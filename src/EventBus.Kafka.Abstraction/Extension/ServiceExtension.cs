using EventBus.Kafka.Abstraction.Abstraction;
using Messages;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Kafka.Abstraction
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddKafkaProducer<TMessage>(
            this IServiceCollection serviceCollection,
            string TopicName) where TMessage : ProjectionMessage
        {
            serviceCollection.AddScoped<IKafkaProducer<TMessage>>(x => new KafkaProducer<TMessage>(TopicName));

            return serviceCollection;
        }

        public static IServiceCollection AddKafkaConsumer<TMessage, TMessageHandler>(
            this IServiceCollection serviceCollection,
            string TopicName,
            string GroupdId) where TMessage : ProjectionMessage where TMessageHandler : class, IMessageHandler<TMessage>
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

            serviceCollection.AddSingleton<IKafkaConsumer<TMessage>>(x =>
               new KafkaConsumer<string, TMessage>(
                GroupdId,
                TopicName,
                 (key, value) => {
                     var messageHandler = serviceCollection.BuildServiceProvider().GetRequiredService<IMessageHandler<TMessage>>();

                     //will be executed synchronously
                     messageHandler.HandleAsync(value);
                 }));

            return serviceCollection;
        }

    }
}
