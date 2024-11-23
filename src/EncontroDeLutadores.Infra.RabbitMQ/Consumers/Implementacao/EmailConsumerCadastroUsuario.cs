using EncontroDeLutadores.Dominio.Interfaces.Servicos.email.Base;
using EncontroDeLutadores.Infra.RabbitMQ.Consumers.Interfaces;
using EncontroDeLutadores.Infra.RabbitMQ.Entidades;
using EncontroDeLutadores.Infra.RabbitMQ.Producers.Implementacao.Email;
using EncontroDeLutadores.Servico.Servicos.Email.ConfirmacaoEmail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Infra.RabbitMQ.Consumers.Implementacao
{
    public class EmailConsumerCadastroUsuario : BackgroundService
    {

        private readonly ConnectionFactory _factory = null;
        private readonly IConfiguration configuration;
        public EmailConsumerCadastroUsuario(IConfiguration configuration)
        {

            _factory = new ConnectionFactory();
            // "guest"/"guest" by default, limited to localhost connections
            _factory.UserName = "guest";
            _factory.Password = "guest";
            _factory.VirtualHost = "/";
            _factory.HostName = "localhost"; //"host.docker.internal";
            this.configuration = configuration;
        }



        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            //var _connection = _factory.CreateConnection();


            //var channel = _connection.CreateModel();

            //channel.QueueDeclare(EQueueEmail.Cadastro.ToString(), false, false, true, null);

            //Consumer(channel);

            return Task.FromResult(true);

        }

        private void Consumer(IModel channel)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(EQueueEmail.Cadastro.ToString(), true, consumer);
        }

        private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var body = JsonSerializer.Deserialize<BodyEmailProducerConsumerCreateUser>(Encoding.UTF8.GetString(e.Body.ToArray()));
            var serviceEmailTemplate = new ServicoEmailTemplateConfirmacao(body.UserID, body.Token, configuration);
            serviceEmailTemplate.SendEmail(body.To);

            Encoding.UTF8.GetString(e.Body.ToArray());

        }
    }
}
