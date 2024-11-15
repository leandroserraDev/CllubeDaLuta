using EncontroDeLutadores.Dominio.Interfaces.Repositorio.MongoDB;
using EncontroDeLutadores.Infra.DBContexto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Infra.Repositorio.MongoDB
{
    public class MongoDBRepositorio<TEntity> : IMongoDBRepositorioBase<TEntity> where TEntity : class
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<TEntity> _collection;


        public MongoDBRepositorio(IOptions<MongoDBSettings> options)
        {
            var mongoClient = new MongoClient(
                options.Value.ConnectionString);

            _mongoDatabase = mongoClient.GetDatabase(
                options.Value.DatabaseName);

            _collection = _mongoDatabase.GetCollection<TEntity>(typeof(TEntity).Name);
        }


        public virtual async Task<bool> DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(id);
            return true;

        }

        public virtual async Task<TEntity> UpdateAsync(string id, TEntity entity)
        {
            await _collection.ReplaceOneAsync(id, entity);
            return entity;

                
        }

        public virtual async Task<TEntity> InsertOneAsync(TEntity entity)
        {

            await _collection.InsertOneAsync(entity);
            return entity;

        }
    }
}
