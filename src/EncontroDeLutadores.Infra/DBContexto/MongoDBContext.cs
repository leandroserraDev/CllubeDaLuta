using EncontroDeLutadores.Dominio.Entidades.perfilLutador;
using EncontroDeLutadores.Infra.Migrations;
using EncontroDeLutadores.Infra.Repositorio.MongoDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncontroDeLutadores.Infra.DBContexto
{
    public class MongoDBContext
    {
        public readonly IMongoDatabase _mongoDBDataBase;

        public MongoDBContext(IOptions<MongoDBSettings> options)
        {
            var mongoClient = new MongoClient(
            options.Value.ConnectionString) ;

            _mongoDBDataBase = mongoClient.GetDatabase(
                options.Value.DatabaseName);


        }

        public IMongoCollection<PerfilLutador> PerfilLutador => _mongoDBDataBase.GetCollection<PerfilLutador>("perfilLutador");

    }
}
