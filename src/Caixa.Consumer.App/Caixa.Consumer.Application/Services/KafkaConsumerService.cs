using Caixa.Consumer.Application.Interfaces;
using Caixa.Consumer.Domain.Entities;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.Options;

namespace Caixa.Consumer.Application.Services
{
    public class KafkaConsumerService : IKafkaConsumerService
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly ISchemaRegistryClient _schemaRegistry;

        public KafkaConsumerService(IOptions<KafkaConfiguration> kafkaConfig)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = kafkaConfig.Value.BootstrapServers,
                GroupId = kafkaConfig.Value.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _schemaRegistry = new CachedSchemaRegistryClient(new SchemaRegistryConfig
            {
                Url = kafkaConfig.Value.SchemaRegistryUrl
            });

            _consumer = new ConsumerBuilder<string, string>(config)
                .SetValueDeserializer(new AvroDeserializer<string>(_schemaRegistry).AsSyncOverAsync())
                .Build();
        }

        public async Task<Message<string, string>> ConsumeAsync(string topic)
        {
            _consumer.Subscribe(topic);
            try
            {
                var cr = _consumer.Consume(CancellationToken.None);
                //return await new Message<string, string> { Value = cr.Message.Value }; // Retorna a mensagem consumida
                return null;
            }
            catch (ConsumeException e)
            {
                throw new Exception("Erro ao consumir mensagem", e);
            }
        }
    }
}
