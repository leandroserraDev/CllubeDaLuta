using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Dominio.Interfaces.Servicos.email.Base
{
    public interface IServicoEmailTemplateBase
    {
        void SendEmail(string to);
    }
}
