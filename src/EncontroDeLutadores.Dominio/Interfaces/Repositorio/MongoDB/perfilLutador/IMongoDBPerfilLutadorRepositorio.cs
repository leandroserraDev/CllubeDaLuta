using EncontroDeLutadores.Dominio.Entidades.perfilLutador;
using EncontroDeLutadores.Dominio.Interfaces.Repositorio.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Dominio.Interfaces.Repositorio.MongoDB.perfilLutador
{
    public interface IMongoDBPerfilLutadorRepositorio : IMongoDBRepositorioBase<PerfilLutador>
    {
    }
}
