﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caixa.Consumer.Domain.Entities
{
    public class KafkaConfiguration
    {
        public string BootstrapServers { get; set; }
        public string SchemaRegistryUrl { get; set; }
        public string GroupId { get; set; }
    }

}
