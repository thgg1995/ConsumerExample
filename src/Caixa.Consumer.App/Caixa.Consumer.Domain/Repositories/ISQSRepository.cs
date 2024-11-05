using Caixa.Consumer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caixa.Consumer.Domain.Repositories
{
    public interface ISQSRepository
    {
        Task PublishAsync(Event @event);
    }
}
