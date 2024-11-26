using EncontroDeLutadores.API.Response;
using EncontroDeLutadores.Testes.API.Integracao.Configuracoes;
using Microsoft.AspNetCore.Mvc.Testing;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Testes.API.Integracao.Login
{
    public class LoginTests : IntegrationTestBase
    {
        public LoginTests(WebApplicationFactory<Program> factory) : base(factory)
        {
        }



        [Fact]
        public async Task Login_with_user_invalid_credentials()
        {

            // Arrange: Dados de um usuário válido
            var loginRequest = new
            {
                email = "user@example.com",
                password = "password123"
            };

            // Act: Faz a chamada ao endpoint de login
            var response = await _client.PostAsJsonAsync("/api/auth", loginRequest);

            // Assert: Verifica se a resposta foi bem-sucedida
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseAux = await response.Content.ReadAsStringAsync();
            // Verifica se a resposta contém false no success
            var result = JsonSerializer.Deserialize<CustomResponse>((await response.Content.ReadAsStringAsync()));
            Assert.False(result.success);

            Assert.IsType<bool>(result.success);

            //verifica se data está null
            Assert.Null(result.data);

            //verificar se tem erros de notificação

            Assert.NotNull(result.errors);

        }

    }
}
