using EncontroDeLutadores.Dominio.Entidades.usuario;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Infra.DBContexto
{
    public class AplicacaoDBContexto : IdentityDbContext<
        Usuario, ApplicationRole, string,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>
    {
        public AplicacaoDBContexto(DbContextOptions<AplicacaoDBContexto> dbContextOptions)
            :base(dbContextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Usuario>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var changeTrack = ChangeTracker.Entries();

            var entities = ChangeTracker.Entries();
            foreach (var entityEntry in entities)
            {

                if (entityEntry.State == EntityState.Modified)
                {


                    if (entityEntry.OriginalValues.Properties.Any(obj => obj.Name == "DataModificado"))
                    {
                        entityEntry.Property("DataModificado").CurrentValue = DateTime.UtcNow;
                    }
                }
                else if (entityEntry.State == EntityState.Added)
                {


                    if (entityEntry.OriginalValues.Properties.Any(obj => obj.Name == "DataCadastro"))
                    {
                        entityEntry.Property("DataCadastro").CurrentValue = DateTime.UtcNow;
                    }


                    if (entityEntry.OriginalValues.Properties.Any(obj => obj.Name == "DataModificado"))
                    {
                        entityEntry.Property("DataModificado").CurrentValue = DateTime.UtcNow;
                    }

                }

            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
