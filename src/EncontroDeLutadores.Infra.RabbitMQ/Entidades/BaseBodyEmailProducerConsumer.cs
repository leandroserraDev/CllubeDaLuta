using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Infra.RabbitMQ.Entidades
{
    public abstract class BaseBodyEmailProducerConsumer
    {
        protected BaseBodyEmailProducerConsumer(string to, string subject)
        {
            To = to;
            Subject = subject;
        }

        public string To { get; private set; }
        public string Subject { get; private set; }


    }
}
