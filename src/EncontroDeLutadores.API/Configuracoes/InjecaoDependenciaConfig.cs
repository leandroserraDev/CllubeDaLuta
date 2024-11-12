using EncontroDeLutadores.Dominio.Interfaces.Servicos.Notificacao;
using EncontroDeLutadores.Servico.Servicos.NotificacaoError;
using Microsoft.AspNetCore.Identity;

namespace EncontroDeLutadores.API.Configuracoes
{
    public static class InjecaoDependenciaConfig
    {
        public static IServiceCollection InjecaoDepenciaConfiguracao(this IServiceCollection services)
        {
            services.AddScoped<INotificacaoErrorServico, NotificacaoErrorServico>();


            return services;
        }
    }
}
