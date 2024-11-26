using EncontroDeLutadores.API.Response;
using EncontroDeLutadores.Dominio.Interfaces.Servicos.Notificacao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EncontroDeLutadores.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotificacaoErrorServico _notificationErrorService;

        protected MainController(INotificacaoErrorServico notificationErrorService)
        {
            _notificationErrorService = notificationErrorService;
        }

        protected async Task<IActionResult> CustomResponse(object result = null)
        {

            if (!await _notificationErrorService.TemNotificacao())
            {
                return Ok(new CustomResponse(true, result, null));
            }

            return BadRequest(new
             CustomResponse(false, result, (_notificationErrorService.Notificacoes().Result)));
        }

    }
}
