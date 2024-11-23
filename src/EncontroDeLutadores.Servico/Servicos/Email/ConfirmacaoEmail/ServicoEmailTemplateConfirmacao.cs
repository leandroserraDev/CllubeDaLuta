using EncontroDeLutadores.Servico.Servicos.Email.Base;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Servico.Servicos.Email.ConfirmacaoEmail
{
    public class ServicoEmailTemplateConfirmacao : ServicoEmailTemplateBase
    {

        private readonly string _linkFrontEnd = string.Empty;
        public ServicoEmailTemplateConfirmacao(string userID,
            string tokenConfirmEmail, IConfiguration configuration) 
            : base( configuration)
        {
            _subject = "Confirmação de E-mail";
            _pathTempalteHTML = "\\Arquivos\\Email\\Templates\\ConfirmarEmail\\confirmarEmail.html";

            _linkFrontEnd = string.Concat(_configuration?["URLFronEnd"],"usuario/confirmar-email", "?userID=", userID,
                    "&token=", tokenConfirmEmail);
        }

 
        protected override string HtmlTemplateReplaceCamposDinamicos(string body)
        {
            body = body.Replace("{{link}}", _linkFrontEnd);

            return body;
        }
    }
}
