using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Dominio.Entidades.objetoValor
{
    public abstract class ObjetoValor
    {
    
        public void AlteraDataCadastro(DateTime data)
        {
            DataCadastro = data;

        }
        public void AlteraDataModificado(DateTime data)
        {
            DataModificado = data;
        }

        public int ID { get; private set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataModificado{ get; set; }
        public bool Ativo { get; private set; }

        protected ObjetoValor(bool ativo)
        {
            Ativo = ativo;
        }
    }
}
