using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RecipesChile.Models;

namespace RecipesChile.Services
{
    public class RecetaServices
    {
        private readonly IMongoCollection<Receta> _receta;

        public RecetaServices(IConfiguration config)
        {
            try
            {
                var client = new MongoClient(config.GetConnectionString("RecetasPaises"));
                var database = client.GetDatabase("RecetasPaises");
                _receta = database.GetCollection<Receta>("recetasChile");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Receta> Get()
        {
            return _receta.Find(receta => true).ToList();
        }

        public Receta Get(string id)
        {
            return _receta.Find<Receta>(receta => receta.Id == id).FirstOrDefault();
        }

        public Receta Create(Receta receta)
        {
            _receta.InsertOne(receta);
            return receta;
        }

        public void Update(string id, Receta recetaIn)
        {
            _receta.ReplaceOne(rec => rec.Id == id, recetaIn);
        }

        public void Remove(Receta recetaIn)
        {
            _receta.DeleteOne(rec => rec.Id == recetaIn.Id);
        }

        public void Remove(string id)
        {
            _receta.DeleteOne(rec => rec.Id == id);
        }
    }
}