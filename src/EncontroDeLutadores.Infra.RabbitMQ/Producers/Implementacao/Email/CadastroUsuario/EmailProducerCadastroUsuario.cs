using EncontroDeLutadores.Infra.RabbitMQ.Entidades;
using EncontroDeLutadores.Infra.RabbitMQ.Producers.Interfaces.Email;
using EncontroDeLutadores.Infra.RabbitMQ.Producers.Interfaces.Email.CadastroUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Infra.RabbitMQ.Producers.Implementacao.Email.CadastroUsuario
{
    public class EmailProducerCadastroUsuario : EmailProducerBase<BodyEmailProducerConsumerCreateUser>,IEmailProducerCadastroUsuario
    {
       

        public EmailProducerCadastroUsuario() : base(EQueueEmail.Cadastro)
        {
        }
    }
}
