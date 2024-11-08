using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Aplicacao.DTOs.Identity
{
    public class UsuarioCriacaoDTO
    {
        public string Nome { get;  set; }
        public string Sobrenome { get;  set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
