using Caixa.Consumer.Application.Interfaces;
using Caixa.Consumer.Domain.Entities;
using Caixa.Consumer.Domain.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caixa.Consumer.Infrastructure.Messaging.Consumers
{
    public class EventClasseService : BackgroundService
    {
        private readonly IKafkaConsumerService _kafkaConsumerService;
        private readonly ISQSRepository _sqsRepository;
        private readonly ILogger<EventClasseService> _logger;

        public EventClasseService(IKafkaConsumerService kafkaConsumerService, ISQSRepository sqsRepository, ILogger<EventClasseService> logger)
        {
            _kafkaConsumerService = kafkaConsumerService;
            _sqsRepository = sqsRepository;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = await _kafkaConsumerService.ConsumeAsync("topic1");
                if (message != null)
                {
                    _logger.LogInformation($"Message received: {message.Value}");
                    await _sqsRepository.PublishAsync(new Event { Data = message.Value });
                }
            }
        }
    }

}
