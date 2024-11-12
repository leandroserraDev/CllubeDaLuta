using EncontroDeLutadores.API.JWT;
using EncontroDeLutadores.Aplicacao.DTOs.Identity;
using EncontroDeLutadores.Dominio.Entidades.usuario;
using EncontroDeLutadores.Dominio.Interfaces.Servicos.email.Base;
using EncontroDeLutadores.Dominio.Interfaces.Servicos.Notificacao;
using EncontroDeLutadores.Servico.Servicos.Email.Base;
using EncontroDeLutadores.Servico.Servicos.Email.ConfirmacaoEmail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace EncontroDeLutadores.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : MainController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly INotificacaoErrorServico _notificacaoErrorServico;
        private readonly IConfiguration _configuration;
        public AuthController(SignInManager<Usuario> signInManager,
            UserManager<Usuario> userManager,
            INotificacaoErrorServico notificacaoErrorServico
,
            IConfiguration configuration)
            : base(notificacaoErrorServico)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _notificacaoErrorServico = notificacaoErrorServico;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginDTO usuarioLoginDTO)
        {

            var result = await _signInManager.PasswordSignInAsync(usuarioLoginDTO.Email, usuarioLoginDTO.Password, false, true);
            if(!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    _notificacaoErrorServico.AddNotificacao("Usuário bloqueado.");

                }
                else
                {
                    _notificacaoErrorServico.AddNotificacao("Usuário ou senha inválida.");

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


            var token = GerarTokenJWT.GerarToken(user);
            return await CustomResponse(token);

        }


    }
}
