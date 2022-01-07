using Confluent.Kafka;
using Settings;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EventBus.Kafka.Abstraction
{
    public class KafkaConsumer<TKey, TValue> : IDisposable, IKafkaConsumer<TKey, TValue> where TValue : class
    {
        private readonly ConsumerConfig _config;
        private IConsumer<TKey, TValue> _consumer;
        private string _topicName;

        public KafkaConsumer(string GroupId, string TopicName)
        {
            _config = new ConsumerConfig
            {
                BootstrapServers = KafkaSettings.BootstrapServers,
                GroupId = GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _topicName = TopicName;

        }

        public Task Consume(Action<TKey, TValue> handler, CancellationToken stoppingToken)
        {
            return Consume(handler, null, null, stoppingToken);
        }


        public async Task Consume(Action<TKey,TValue> handler, int? partition, int? offset, CancellationToken stoppingToken)
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            _consumer = new ConsumerBuilder<TKey, TValue>(_config).SetValueDeserializer(new KafkaDeserializer<TValue>(jsonSerializerOptions)).Build();

            await Task.Run(() => StartConsumerLoop(handler, partition, offset, stoppingToken), stoppingToken);
        }

        public void Close()
        {
            _consumer.Close();
        }

        public void Dispose()
        {
            _consumer.Dispose();
        }

        private Task StartConsumerLoop(Action<TKey, TValue> handler, int? partitionNumber, int? offset, CancellationToken cancellationToken)
        {
            var partition = partitionNumber ?? 0;

            if (!partitionNumber.HasValue && !offset.HasValue)
            {
                _consumer.Subscribe(_topicName);
            }
            else
            {
                if (offset.HasValue)
                {
                    _consumer.Assign(new TopicPartitionOffset(_topicName, partition, offset.Value));
                }
                else
                {
                    _consumer.Assign(new TopicPartition(_topicName, partition));
                }
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(cancellationToken);

                    if (result != null)
                    {
                        handler(result.Message.Key, result.Message.Value);
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Consume error: {e.Error.Reason}");

                    if (e.Error.IsFatal)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unexpected error: {e}");
                    break;
                }
            }

            return Task.FromResult(true);
        }
    }
}
