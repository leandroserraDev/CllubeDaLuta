using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Infra.RabbitMQ.Consumers.Interfaces
{
    public interface IEmailConsumerBase<TEntity> where TEntity : class
    {

    }
}
