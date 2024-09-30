using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DoAnNoSQL.Entities
{
    public class Products
    {
        [BsonId]
        [BsonElement("_id")]
        public string? _id {  get; set; }

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string? name { get; set; }

        [BsonElement("description"), BsonRepresentation(BsonType.String)]
        public string? description { get; set; }

        [BsonElement("category"), BsonRepresentation(BsonType.String)]
        public string? category { get; set; }

        [BsonElement("images")]
        public string[]? images { get; set; }

        [BsonElement("rating"), BsonRepresentation(BsonType.Double)]
        public double? rating { get; set; }

        [BsonElement("total_ratings"), BsonRepresentation(BsonType.Int32)]
        public int? total_ratings { get; set; }

        [BsonElement("reviews")]
        public Reviews[]? reviews { get; set; } = Array.Empty<Reviews>();
    }
}
