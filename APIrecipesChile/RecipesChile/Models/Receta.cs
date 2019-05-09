using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace RecipesChile.Models
{
    /// <summary>
    /// Receta.
    /// </summary>
    public class Receta
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("nombre")]
        public string Nombre { get; set; }
        [BsonElement("insumos")]
        public Insumos Insumos { get; set; }
        [BsonElement("tiempo")]
        public string Tiempo { get; set; }
        [BsonElement("cantidad")]
        public string Cantidad { get; set; }
        [BsonElement("preparacion")]
        public string Preparacion { get; set; }
        [BsonElement("dificultad")]
        public string Dificultad { get; set; }
        [BsonElement("pais")]
        public string Pais { get; set; } 
        [BsonElement("imagen")]
        public string Imagen { get; set; }
    }
}
