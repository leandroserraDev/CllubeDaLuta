using EncontroDeLutadores.Dominio.Entidades.usuario;
using EncontroDeLutadores.Infra.DBContexto;
using Microsoft.AspNetCore.Identity;

namespace EncontroDeLutadores.API.Configuracoes
{
    public static class IdentityConfig
    {
        public static IServiceCollection IdentityConfiguracao(this IServiceCollection services)
        {
            services.AddIdentity<Usuario, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddRoles<ApplicationRole>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AplicacaoDBContexto>();

            services.AddDataProtection();
            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;


                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                // Default User settings.
                options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -._@+";
                options.User.RequireUniqueEmail = true;

            });

            services.Configure<PasswordHasherOptions>(option =>
            {
                option.IterationCount = 12000;
            });

            return services;
        }
    }
}
