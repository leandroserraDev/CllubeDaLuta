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
    public class IdentityTests : IntegrationTestBase
    {
        public IdentityTests(GenericAPIFactory genericApiFactory) : base(genericApiFactory)
        {
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

            var createdUser = await response.Content.ReadFromJsonAsync<dynamic>();

            // validar retorno true
            createdUser.success.Should().BeTrue();

            //validar email no retorno igual ao cadastrado.
            createdUser.data.Should().Be("leandroserra@testeintegracao.com");


            
        }
    }
}
