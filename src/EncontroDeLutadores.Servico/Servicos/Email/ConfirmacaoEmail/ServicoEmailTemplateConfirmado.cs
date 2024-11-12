using EncontroDeLutadores.Servico.Servicos.Email.Base;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Servico.Servicos.Email.ConfirmacaoEmail
{
    public class ServicoEmailTemplateConfirmado : ServicoEmailTemplateBase
    {
        public ServicoEmailTemplateConfirmado(IConfiguration configuration) 
            : base( configuration)
        {
            _subject = "E-mail Confirmado";
            _pathTempalteHTML = "\\Arquivos\\Email\\Templates\\ConfirmarEmail\\emailConfirmado.html";
        }

        protected override string HtmlTemplateReplaceCamposDinamicos(string body)
        {
            body = body.Replace("{{nomeUsuario}}", "Leandro Serra");
            return body;
        }

    }
}
