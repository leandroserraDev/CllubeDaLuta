using EncontroDeLutadores.Dominio.Entidades.perfilLutador;
using EncontroDeLutadores.Dominio.Interfaces.Repositorio.MongoDB;
using EncontroDeLutadores.Dominio.Interfaces.Repositorio.MongoDB.perfilLutador;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EncontroDeLutadores.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesteMongoDBController : ControllerBase
    {

        private readonly ILogger<TesteMongoDBController> _logger;

        public TesteMongoDBController(ILogger<TesteMongoDBController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SavePerfilLutador([FromServices]IMongoDBRepositorioBase<PerfilLutador> mongoDBPerfilLutadorRepositorio,  [FromBody]PerfilLutador perfilLutador)
        {
            _logger.LogInformation("Salvando no mongo DB");
           var result =  await mongoDBPerfilLutadorRepositorio.InsertOneAsync(perfilLutador);
            return Ok(result);
        }
    }
}
