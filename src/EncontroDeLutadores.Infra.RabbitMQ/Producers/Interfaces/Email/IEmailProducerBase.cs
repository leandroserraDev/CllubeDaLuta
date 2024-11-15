using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Infra.RabbitMQ.Producers.Interfaces.Email
{
    public interface IEmailProducerBase<TEntity> where  TEntity : class
    {
        Task SendProducer(TEntity body);
    }
}
