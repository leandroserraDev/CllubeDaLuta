using EncontroDeLutadores.Infra.DBContexto;
using EncontroDeLutadores.Testes.API.Integracao.Configuracoes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Testes.API.Integracao
{
     public class IntegrationTestBase : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly HttpClient _client;
        protected readonly WebApplicationFactory<Program> _factory;

        public IntegrationTestBase(WebApplicationFactory<Program> factory)
        {
            // Configura o WebApplicationFactory para usar um banco de dados em memória
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Substitui o contexto de banco de dados com um banco de dados em memória
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<AplicacaoDBContexto>));

                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddDbContext<AplicacaoDBContexto>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb");
                    });
                    var dbContext = services.BuildServiceProvider().GetService<AplicacaoDBContexto>();
                    dbContext.Database.Migrate();

                    // Outras dependências podem ser mockadas aqui, se necessário
                });
            });

            _client = _factory.CreateClient();
        }
    }

}
