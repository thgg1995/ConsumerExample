using Amazon.SQS;
using Amazon.SQS.Model;
using Caixa.Consumer.Domain.Entities;
using Caixa.Consumer.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caixa.Consumer.Infrastructure.Repositories.Messaging
{
    public class SQSRepository : ISQSRepository
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly string _queueUrl;

        public SQSRepository(IAmazonSQS sqsClient, IConfiguration configuration)
        {
            _sqsClient = sqsClient;
            _queueUrl = configuration["SQS:QueueUrl"] ?? string.Empty;
        }

        public async Task PublishAsync(Event @event)
        {
            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = @event.Data
            };

            await _sqsClient.SendMessageAsync(sendMessageRequest);
        }
    }

}
