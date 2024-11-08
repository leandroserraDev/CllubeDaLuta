using EncontroDeLutadores.API.Configuracoes;
using EncontroDeLutadores.Dominio.Entidades.usuario;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EncontroDeLutadores.API.JWT
{
    public static class GerarTokenJWT
    {
        public static async Task<string> GerarToken(Usuario usuario)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var appSettings = builder.GetSection("AppSetting").Get<AppSettings>();

            var identityClaims = new ClaimsIdentity();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sid, usuario.Id),
                new Claim(JwtRegisteredClaimNames.Name, usuario.Nome + " " + usuario.Sobrenome),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.Now).ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.Now).ToString())
            };

            usuario.UserRoles.ToList().ForEach(obj =>
            {
                var newClaim = new Claim("role", obj.Role.Name);
                claims.Add(newClaim);
            });

            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Segredo);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = appSettings.Emissor,
                Audience = appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);


            return await Task.FromResult(encodedToken);
        }

        private static long ToUnixEpochDate(DateTime date) => new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds();


    }
}
