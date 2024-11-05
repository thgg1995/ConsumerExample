using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caixa.Consumer.Application.Interfaces
{
    public interface IKafkaConsumerService
    {
        Task<Message<string, string>> ConsumeAsync(string topic);
    }
}
