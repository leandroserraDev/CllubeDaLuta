using EncontroDeLutadores.Dominio.Interfaces.Servicos.Notificacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Servico.Servicos.NotificacaoError
{
    public class NotificacaoErrorServico : INotificacaoErrorServico
    {
        private readonly IList<string> _errors = new List<string>();

        public void AddNotificacao(string notificacao)
        {
            _errors.Add(notificacao);
        }

        public async Task<IList<string>> Notificacoes()
        {
            return await Task.FromResult(_errors);
        }

        public async Task<bool> TemNotificacao()
        {
            return await Task.FromResult(_errors.Any());
        }
    }
}
