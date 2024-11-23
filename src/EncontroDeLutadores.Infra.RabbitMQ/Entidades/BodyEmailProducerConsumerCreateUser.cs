using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Infra.RabbitMQ.Entidades
{
    public class BodyEmailProducerConsumerCreateUser : BaseBodyEmailProducerConsumer
    {
        public string UserID { get; private set; }
        public string Token { get; private set; }

        public BodyEmailProducerConsumerCreateUser(string to, string subject, string token) : base(to, subject)
        {
            Token = token;
        }
    }
}
