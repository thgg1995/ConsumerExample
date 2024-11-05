using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caixa.Consumer.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public DateTime Timestamp { get; set; }
    }

}
