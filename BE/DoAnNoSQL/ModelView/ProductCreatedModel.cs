using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DoAnNoSQL.ModelView
{
    public class ProductCreatedModel
    {
        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string? name { get; set; }

        [BsonElement("description"), BsonRepresentation(BsonType.String)]
        public string? description { get; set; }

        [BsonElement("category"), BsonRepresentation(BsonType.String)]
        public string? category { get; set; }

        [BsonElement("images")]
        public string[]? images { get; set; }
    }
}
