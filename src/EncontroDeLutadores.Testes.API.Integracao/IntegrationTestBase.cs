using EncontroDeLutadores.Testes.API.Integracao.Configuracoes;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Testes.API.Integracao
{
     public class IntegrationTestBase : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        protected readonly HttpClient _client;

        public IntegrationTestBase(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }
    }
}
