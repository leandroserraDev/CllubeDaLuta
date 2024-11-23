using EncontroDeLutadores.Infra.RabbitMQ.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Infra.RabbitMQ.Producers.Interfaces.Email.CadastroUsuario
{
    public interface IEmailProducerCadastroUsuario : IEmailProducerBase<BodyEmailProducerConsumerCreateUser>
    {
    }
}
