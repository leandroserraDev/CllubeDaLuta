using EncontroDeLutadores.API.JWT;
using EncontroDeLutadores.Aplicacao.DTOs.Identity;
using EncontroDeLutadores.Dominio.Entidades.usuario;
using EncontroDeLutadores.Dominio.Interfaces.Servicos.Notificacao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EncontroDeLutadores.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : MainController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly INotificacaoErrorServico _notificacaoErrorServico;

        public AuthController(SignInManager<Usuario> signInManager, 
            UserManager<Usuario> userManager, 
            INotificacaoErrorServico notificacaoErrorServico)
            :base(notificacaoErrorServico)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _notificacaoErrorServico = notificacaoErrorServico;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginDTO usuarioLoginDTO)
        {

            var result = await _signInManager.PasswordSignInAsync(usuarioLoginDTO.Email, usuarioLoginDTO.Password, false, true);
            if(!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    _notificacaoErrorServico.AddNotificacao("Usuário bloqueado");

                }else if (result.IsNotAllowed)
                {
                    _notificacaoErrorServico.AddNotificacao("Usuário não confirmado, por favor confirme o e-mail");

                }
                else
                {
                    _notificacaoErrorServico.AddNotificacao("Usuário ou senha inválida");

                }

                return await CustomResponse();
            }

            var user = await _userManager.Users.
                Where(obj => obj.Email.Equals(usuarioLoginDTO.Email))
                .Include(obj => obj.UserRoles)
                .FirstOrDefaultAsync();

            var usuarioConfirmado = await _userManager.IsEmailConfirmedAsync(user);

            if (!usuarioConfirmado)
            {
                _notificacaoErrorServico.AddNotificacao("Usuário precisa confirmar o e-mail");
                return await CustomResponse();
            }


            var token = await GerarTokenJWT.GerarToken(user);
            return await CustomResponse(token);

        }



    }
}
