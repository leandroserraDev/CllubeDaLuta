using EncontroDeLutadores.Dominio.Interfaces.Servicos.email.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Xml.Linq;

namespace EncontroDeLutadores.Servico.Servicos.Email.Base
{
    public abstract class ServicoEmailTemplateBase : IServicoEmailTemplateBase
    {
        private readonly SmtpClient _smtpClient;
        private readonly MailMessage _mailMessage = new MailMessage();
        protected readonly IConfiguration _configuration;
        protected string _pathTempalteHTML = string.Empty;
        protected string _subject = string.Empty;


        public ServicoEmailTemplateBase(


            IConfiguration configuration
            )
        {
            _configuration = configuration;

            // Host para envio de email
            var host = _configuration?["ConfigurationMAILTrap:Host"];
            var port = _configuration?["ConfigurationMAILTrap:Port"];
            var userName = _configuration?["ConfigurationMAILTrap:UserName"];
            var password = _configuration?["ConfigurationMAILTrap:Password"];

            //subject do template
            _subject = "Template Base";

            _smtpClient = new SmtpClient(host, Convert.ToInt32(port))
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = true
            };



        }

        protected virtual string HtmlTemplateReplaceCamposDinamicos(string body)
        {
            throw new NotImplementedException("Html replace method not implemented");
        }

        private string ReplaceLogoTemplate(string body)
        {
            var base64Logo = File.ReadAllBytes($"{Environment.CurrentDirectory}\\Arquivos\\Imagens\\fight-png-17964.png");

            body = body.Replace("{{logo}}", Convert.ToBase64String(base64Logo));
            return body;
        }


        private string HtmlTemplate()
        {
            var body = string.Empty;


            var path = string.Concat(Environment.CurrentDirectory, _pathTempalteHTML);

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    body = sr.ReadToEnd();
                }

                return ReplaceLogoTemplate(HtmlTemplateReplaceCamposDinamicos(body));


            }
            throw new FileNotFoundException("Template file for send e-mail not created") ;

        }


        public async void SendEmail(string to)
        {
            _mailMessage.From = new MailAddress("leandroteste@teste.com");
            _mailMessage.Subject = _subject;
            _mailMessage.To.Add(to);
            _mailMessage.IsBodyHtml = true;

            var body = HtmlTemplate();

            _mailMessage.Body = body;
            await _smtpClient.SendMailAsync(_mailMessage);

        }
    }
}
