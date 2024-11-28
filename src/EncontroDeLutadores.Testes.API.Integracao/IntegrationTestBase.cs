using EncontroDeLutadores.Infra.DBContexto;
using EncontroDeLutadores.Testes.API.Integracao.Configuracoes;
using EncontroDeLutadores.Testes.API.Integracao.Factor;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Testes.API.Integracao
{
     public class IntegrationTestBase : IClassFixture<GenericAPIFactory>
    {
        private readonly GenericAPIFactory _genericApiFactory;
        protected readonly HttpClient _client;

        public IntegrationTestBase(GenericAPIFactory genericApiFactory)
        {
            _genericApiFactory = genericApiFactory;
            _client = _genericApiFactory.CreateClient(); 
        }



    }

}
