using System.Text.Json;
using Confluent.Kafka;

namespace TransactionAggregationApi.Application.Services;

public class KafkaProducerService : IDisposable
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaProducerService> _logger;

    public KafkaProducerService(IConfiguration configuration, ILogger<KafkaProducerService> logger)
    {
        _logger = logger;
        var config = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"]
                ?? throw new InvalidOperationException("Kafka:BootstrapServers not configured"),
            CompressionType = CompressionType.Lz4,
            Acks = Acks.All,
            EnableIdempotence = true
        };

        var saslUsername = configuration["Kafka:SaslUsername"];
        if (!string.IsNullOrWhiteSpace(saslUsername))
        {
            config.SecurityProtocol = SecurityProtocol.SaslSsl;
            config.SaslMechanism = SaslMechanism.ScramSha512;
            config.SaslUsername = saslUsername;
            config.SaslPassword = configuration["Kafka:SaslPassword"];
        }

        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task<DeliveryResult<string, string>> ProduceAsync<T>(string topic, string key, T message, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(message);
        var result = await _producer.ProduceAsync(topic, new Message<string, string> { Key = key, Value = json }, cancellationToken);
        _logger.LogDebug("Produced to {Topic} partition {P} offset {O}", topic, result.Partition, result.Offset);
        return result;
    }

    public void Flush(TimeSpan timeout) => _producer.Flush(timeout);

    public void Dispose() => _producer?.Dispose();
}
