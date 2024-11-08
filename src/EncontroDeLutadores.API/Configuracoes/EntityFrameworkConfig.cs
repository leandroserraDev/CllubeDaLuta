using EncontroDeLutadores.Infra.DBContexto;
using Microsoft.EntityFrameworkCore;

namespace EncontroDeLutadores.API.Configuracoes
{
    public static class EntityFrameworkConfig
    {
        public static IServiceCollection EntityFrameworkConfiguracao(this IServiceCollection _services)
        {
            var builder = new ConfigurationBuilder()
                              .AddJsonFile("appsettings.json")
                              .Build();

            _services.AddDbContext<AplicacaoDBContexto>(options =>
            {
                options.UseNpgsql(builder.GetConnectionString("Connection"));
            });

            return _services;

        }
    }
}
