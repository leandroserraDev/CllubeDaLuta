using EncontroDeLutadores.Testes.API.Integracao.Configuracoes;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Testes.API.Integracao.Login
{
    public class LoginTestsIntegration : IntegrationTestBase
    {
        public LoginTestsIntegration(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Login_with_user_not_found()
        {

            var loginRequest = new
            {
                email = "string",
                password = "string"
            };

           var result =  await _client.PostAsJsonAsync("/api/Auth", loginRequest);

            //Asserts
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            var resultAux = await result.Content.ReadAsStringAsync();
            Assert.NotNull(null);
        }

    }
}
