using EncontroDeLutadores.Aplicacao.DTOs.Identity;
using EncontroDeLutadores.Dominio.Entidades.usuario;
using EncontroDeLutadores.Dominio.Interfaces.Servicos.Notificacao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace EncontroDeLutadores.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : MainController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly INotificacaoErrorServico _notificacaoErrorServico;

        public IdentityController(UserManager<Usuario> userManager, INotificacaoErrorServico notificacaoErrorServico)
            :base(notificacaoErrorServico)
        {
            _userManager = userManager;
            _notificacaoErrorServico = notificacaoErrorServico;
        }

        /// <summary>
        /// Cadastrar novo usuário do tipo lutador no sistema
        /// </summary>
        /// <param name="usuarioCriadoDTO"></param>
        /// <returns></returns>
        [HttpPost("lutador")]
        public async Task<IActionResult> Criar(UsuarioCriacaoDTO usuarioCriadoDTO)
        {

            var user = await CriarUsuario(usuarioCriadoDTO);

            if (user != null)
            {
                // inserir role do usuario lutador
                var resultInsertRole = await _userManager.AddToRoleAsync(user, "Lutador");
                if (!resultInsertRole.Succeeded)
                {
                    resultInsertRole.Errors.ToList().ForEach(obj =>
                    {
                        _notificacaoErrorServico.AddNotificacao(obj.Description);

                    });

                    return await CustomResponse();
                }

                //gerar token e enviar e-mail para usuario com o link de confirmação
                var tokenEmail = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                return await CustomResponse();
            }

            return await CustomResponse();
        }

        /// <summary>
        /// Confirmar e-mail do usuário
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("confirmar-email/{userID}/{token}")]
        public async Task<IActionResult> ConfirmarEmail(string userID, string token)
        {
            var user = await _userManager.FindByIdAsync(userID);
            if (user != null)
            {
                return NotFound();
            }

            //validacao se usuario já está com e-mail confirmado
            if (await _userManager.IsEmailConfirmedAsync(user))
            {
                return BadRequest();

            }

            var resultConfirmarEmail = await _userManager.ConfirmEmailAsync(user, token);
            if (resultConfirmarEmail.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }


        private async Task<Usuario> CriarUsuario(UsuarioCriacaoDTO usuarioCriadoDTO)
        {
            var usuario = new Usuario()
            {
                Nome = usuarioCriadoDTO.Nome,
                Sobrenome = usuarioCriadoDTO.Sobrenome,
                Email = usuarioCriadoDTO.Email,
                UserName = usuarioCriadoDTO.Email
            };

            var result = await _userManager.CreateAsync(usuario, usuarioCriadoDTO.Password);

            if (!result.Succeeded)
            {

                result.Errors.ToList().ForEach(obj =>
                {
                    _notificacaoErrorServico.AddNotificacao(obj.Description);

                });

                return null;
            }
            return await Task.FromResult(usuario);


        }

    }
}
