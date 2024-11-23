using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Aplicacao.DTOs.Identity.confirmarEmail
{
    public record ConfirmarEmailDTO(string userID, string token);

}
