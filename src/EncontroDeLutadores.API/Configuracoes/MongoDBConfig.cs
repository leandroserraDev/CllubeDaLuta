using EncontroDeLutadores.Infra.Repositorio.MongoDB;

namespace EncontroDeLutadores.API.Configuracoes
{
    public static class MongoDBConfig
    {
        public static IServiceCollection MongoDBConfiguration(this IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                           .AddJsonFile("appsettings.json")
                           .Build();
            services.Configure<MongoDBSettings>(builder.GetSection("MongoDBSettings"));
            return services;
        }
    }
}
