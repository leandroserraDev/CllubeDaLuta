using EncontroDeLutadores.Dominio.Interfaces.Repositorio.MongoDB;
using EncontroDeLutadores.Dominio.Interfaces.Repositorio.MongoDB.perfilLutador;
using EncontroDeLutadores.Dominio.Interfaces.Servicos.Notificacao;
using EncontroDeLutadores.Infra.RabbitMQ.Producers.Implementacao.Email;
using EncontroDeLutadores.Infra.RabbitMQ.Producers.Implementacao.Email.CadastroUsuario;
using EncontroDeLutadores.Infra.RabbitMQ.Producers.Interfaces.Email;
using EncontroDeLutadores.Infra.RabbitMQ.Producers.Interfaces.Email.CadastroUsuario;
using EncontroDeLutadores.Infra.Repositorio.MongoDB;
using EncontroDeLutadores.Servico.Servicos.NotificacaoError;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace EncontroDeLutadores.API.Configuracoes
{
    public static class InjecaoDependenciaConfig
    {
        public static IServiceCollection InjecaoDepenciaConfiguracao(this IServiceCollection services)
        {

            var builder = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json")
                  .Build();


            services.AddScoped<INotificacaoErrorServico, NotificacaoErrorServico>();




            //MongoDB

            // MongoDB configuration
            services.AddScoped(typeof(IMongoDBRepositorioBase<>), typeof(MongoDBRepositorio<>));


            ///RabbitMQ 

            services.AddScoped(typeof(IEmailProducerBase<>), typeof(EmailProducerBase<>));
            services.AddScoped<IEmailProducerCadastroUsuario, EmailProducerCadastroUsuario>();

            return services;
        }
    }
}
