using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EncontroDeLutadores.API.Response;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using EncontroDeLutadores.Testes.API.Integracao.Factor;

namespace EncontroDeLutadores.Testes.API.Integracao.Identity
{
    public class IdentityTests : IClassFixture<GenericAPIFactory>
    {
        private readonly GenericAPIFactory _genericApiFactory;
        private readonly HttpClient _client;

        public IdentityTests(GenericAPIFactory genericApiFactory)
        {
            _genericApiFactory = genericApiFactory;
            _client = _genericApiFactory.CreateClient();
        }

        [Fact]
        public async Task Create_User_With_Valid()
        {
            // Arrange: Dados de um usuário válido
             var loginRequest = new
            {
                nome = "Leandro",
                sobrenome = "Serra",
                email = "leandroserra@testeintegracao.com",
                password = "Senha1234#"
            };

            // Act: Faz a chamada ao endpoint de login
            var response = await _client.PostAsJsonAsync("/api/identity/lutador", loginRequest);

            // Assert: Verifica se a resposta foi bem-sucedida
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseAux = await response.Content.ReadAsStringAsync();
            // Verifica se a resposta contém false no success
            var result = JsonSerializer.Deserialize<CustomResponse?>((await response.Content.ReadAsStringAsync()));
            Assert.True(result.success);

            Assert.IsType<bool>(result.success);

            //verifica se data está null
            Assert.NotNull(result.data);

            //verificar se tem erros de notificação

            Assert.Null(result.errors);
        }
    }
}
