using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Dominio.Interfaces.Repositorio.MongoDB
{
    public interface IMongoDBRepositorioBase<TEntity> where TEntity : class
    {
        Task<TEntity> UpdateAsync(string id, TEntity entity);
        Task<bool> DeleteAsync(string id);
        Task<TEntity> InsertOneAsync(TEntity entity);


    }
}
