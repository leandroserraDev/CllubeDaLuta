using EncontroDeLutadores.API.JWT;
using EncontroDeLutadores.Aplicacao.DTOs.Identity;
using EncontroDeLutadores.Aplicacao.DTOs.Identity.confirmarEmail;
using EncontroDeLutadores.Dominio.Entidades.usuario;
using EncontroDeLutadores.Dominio.Interfaces.Servicos.Notificacao;
using EncontroDeLutadores.Infra.RabbitMQ.Entidades;
using EncontroDeLutadores.Infra.RabbitMQ.Producers.Interfaces.Email.CadastroUsuario;
using EncontroDeLutadores.Servico.Servicos.Email.ConfirmacaoEmail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
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
        private readonly IConfiguration _configuration;
        public IdentityController(UserManager<Usuario> userManager, INotificacaoErrorServico notificacaoErrorServico, IConfiguration configuration)
            : base(notificacaoErrorServico)
        {
            _userManager = userManager;
            _notificacaoErrorServico = notificacaoErrorServico;
            _configuration = configuration;
        }

        /// <summary>
        /// Cadastrar novo usuário do tipo lutador no sistema
        /// </summary>
        /// <param name="usuarioCriadoDTO"></param>
        /// <returns></returns>
        [HttpPost("lutador")]
        public async Task<IActionResult> Criar([FromServices]IEmailProducerCadastroUsuario emailProducerCadastroUsuario,  UsuarioCriacaoDTO usuarioCriadoDTO)
        {


            var tokenEmail = string.Empty;
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
                tokenEmail = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            }
            else
            {
                return await CustomResponse();
            }
            var tokenEncode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(tokenEmail));

            ////
            var producerEmailCadastrar = new BodyEmailProducerConsumerCreateUser(user.Email, "Confirmar E-email", tokenEncode);
            await emailProducerCadastroUsuario.SendProducer(producerEmailCadastrar);



            return await CustomResponse(new {nome= user.Nome, sobrenome = user.Sobrenome, email =  user.Email});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="confirmarEmailDTO"></param>
        /// <returns></returns>
        [HttpPost("confirmar-email")]
        public async Task<IActionResult> ConfirmarEmail(ConfirmarEmailDTO confirmarEmailDTO)
        {

            if (string.IsNullOrEmpty(confirmarEmailDTO.userID) || string.IsNullOrEmpty(confirmarEmailDTO.token))
            {
                _notificacaoErrorServico.AddNotificacao("Usuário ou token vazio");
                return await CustomResponse();
            }
            var user = await _userManager.FindByIdAsync(confirmarEmailDTO.userID);
            if (user == null)
            {
                return NotFound();
            }

            //validacao se usuario já está com e-mail confirmado
            if (await _userManager.IsEmailConfirmedAsync(user))
            {
                _notificacaoErrorServico.AddNotificacao("E-mail já está confirmado");

                return await CustomResponse();

            }

            var tokenFromBase64 = Convert.FromBase64String(confirmarEmailDTO.token);
            var tokenDecode = Encoding.UTF8.GetString(tokenFromBase64);

            var resultConfirmarEmail = await _userManager.ConfirmEmailAsync(user, tokenDecode);
            if (!resultConfirmarEmail.Succeeded)
            {
                foreach (var item in resultConfirmarEmail.Errors)
                {
                    _notificacaoErrorServico.AddNotificacao(item.Description);
                }
            }

            new ServicoEmailTemplateConfirmado(_configuration).SendEmail("leandroserra@teste.com");

            return await CustomResponse();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        [HttpGet("reenviar-email-confirmacao/{userEmail}")]
        public async Task<IActionResult> ReenviarConfirmacaoEmail(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                _notificacaoErrorServico.AddNotificacao("Usuário não encontrado");
                return await CustomResponse();
            }

            var tokenEmail = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            new ServicoEmailTemplateConfirmacao(user.Id, tokenEmail, _configuration)
                .SendEmail(userEmail);

            return await CustomResponse();

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
