using EncontroDeLutadores.Infra.DBContexto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Testes.API.Integracao.Configuracoes
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AplicacaoDBContexto>));
            
            
                if(descriptor  != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<AplicacaoDBContexto>(
                    options =>
                    {
                        options.UseInMemoryDatabase("DataBaseInMemory");
                    });


                // Cria o banco com migrations
                using var scope = services.BuildServiceProvider().CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AplicacaoDBContexto>();
                context.Database.EnsureCreated();
            });


        }
    }
}
