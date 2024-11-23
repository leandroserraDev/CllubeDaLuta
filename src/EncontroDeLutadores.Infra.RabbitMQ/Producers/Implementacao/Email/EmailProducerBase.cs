using EncontroDeLutadores.Dominio.Interfaces.Servicos.email.Base;
using EncontroDeLutadores.Infra.RabbitMQ.Producers.Interfaces.Email;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Infra.RabbitMQ.Producers.Implementacao.Email
{
    public class EmailProducerBase<TEntity> : IEmailProducerBase<TEntity> where TEntity : class
    {
        private readonly IConnection _connection;
        private readonly EQueueEmail _eQueueEmail;
        private readonly IServicoEmailTemplateBase _servicoEmailTemplateBase;

        public EmailProducerBase(EQueueEmail eQueueEmail)
        {
            ConnectionFactory factory = new ConnectionFactory();
            // "guest"/"guest" by default, limited to localhost connections
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.VirtualHost = "/";
            factory.HostName = "localhost";

            _connection = factory.CreateConnection();
            _eQueueEmail = eQueueEmail;

        }
        public async Task SendProducer(TEntity body)
        {
            var channel = _connection.CreateModel();

            channel.QueueDeclare(_eQueueEmail.ToString(), false, false, true, null);

            channel.BasicPublish("", _eQueueEmail.ToString(), null, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(body)));

        }
    }
}
