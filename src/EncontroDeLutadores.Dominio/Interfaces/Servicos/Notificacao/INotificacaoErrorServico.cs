using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Dominio.Interfaces.Servicos.Notificacao
{
    public interface INotificacaoErrorServico
    {
        Task<IList<string>> Notificacoes();
        void AddNotificacao(string notificacao);

        Task<bool> TemNotificacao();
    }
}
