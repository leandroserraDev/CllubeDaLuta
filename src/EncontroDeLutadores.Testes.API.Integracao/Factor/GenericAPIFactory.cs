
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using EncontroDeLutadores.Infra.DBContexto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.RabbitMq;

namespace EncontroDeLutadores.Testes.API.Integracao.Factor
{
    public class GenericAPIFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {

        protected readonly IContainer _postgresContainer = new ContainerBuilder()
                         .WithImage("postgres:16")
                         .WithExposedPort(5432) // Porta padrão do PostgreSQL
                         .WithPortBinding(5432, 5432)
                         .WithEnvironment("POSTGRES_PASSWORD", "MarcaDagua1234")
                         .WithEnvironment("POSTGRES_USER", "sa")
                         .WithEnvironment("POSTGRES_DB", "clubedaluta")
                         .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
                         .Build();

        protected readonly IContainer _rabbitMqContainer = new ContainerBuilder()
            .WithImage("rabbitmq:3-management") // Imagem com interface de gerenciamento
            .WithExposedPort(5672) // Porta padrão do RabbitMQ
            .WithExposedPort(15672) // Porta para o painel de administração
            .WithPortBinding(5672, 5672)
            .WithPortBinding(15672, 15672)
            .WithEnvironment("RABBITMQ_DEFAULT_USER", "guest") // Usuário padrão
            .WithEnvironment("RABBITMQ_DEFAULT_PASS", "guest") // Senha padrão
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5672))
            .Build();



        public async Task InitializeAsync()
        {
            if (_postgresContainer.State != TestcontainersStates.Running
                     &&
                     _postgresContainer.State != TestcontainersStates.Created
                     &&
                     _postgresContainer.State != TestcontainersStates.Paused


                     )
            {
                await _postgresContainer.StartAsync();

            }
            if (_rabbitMqContainer.State != TestcontainersStates.Running
                  &&
                  _rabbitMqContainer.State != TestcontainersStates.Created
                  &&
                  _rabbitMqContainer.State != TestcontainersStates.Paused


                  )
            {
                await _rabbitMqContainer.StartAsync();

            }
            ;
        }

        public new async Task DisposeAsync()
        {

            await StopAsync();

        }

        public Task StopAsync()
        {
            return _postgresContainer.DisposeAsync().AsTask();
        }

        protected override async void ConfigureWebHost(IWebHostBuilder builder)
        {


            builder.ConfigureServices(async services =>
            {
                var dbContextOptionsDescriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DbContextOptions<AplicacaoDBContexto>));

                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(AplicacaoDBContexto));

                services.Remove(dbContextDescriptor);
                services.Remove(dbContextOptionsDescriptor);

                services.AddDbContext<AplicacaoDBContexto>(options =>
                {
                    options.UseNpgsql($"Host={_postgresContainer.Hostname};User ID=sa; Password=MarcaDagua1234;Database=EncontroDeLutadores;Pooling=true");
                });


                var dbContext = services.BuildServiceProvider().GetService<AplicacaoDBContexto>();
                dbContext.Database.Migrate();
            });


        }

    }
}
