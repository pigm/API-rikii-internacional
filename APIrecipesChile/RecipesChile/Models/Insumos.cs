using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace RecipesChile.Models
{
    /// <summary>
    /// Insumos.
    /// </summary>
    public class Insumos
    {
        [BsonElement("ingredientes")]
        public List<string> ingredientes { get; set; }
    }
}
