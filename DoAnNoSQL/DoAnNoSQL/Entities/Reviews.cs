using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DoAnNoSQL.Entities
{
    public class Reviews
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }

        [BsonElement("product_id"), BsonRepresentation(BsonType.String)]
        public string? product_id { get; set; }

        [BsonElement("user_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? user_id { get; set; }

        [BsonElement("rating"), BsonRepresentation(BsonType.Int32)]
        public int? rating { get; set; }

        [BsonElement("content"), BsonRepresentation(BsonType.String)]
        public string? content { get; set; }

        [BsonElement("created_at"), BsonRepresentation(BsonType.DateTime)]
        public DateTime created_at { get; set; }

        [BsonElement("updated_at"), BsonRepresentation(BsonType.DateTime)]
        public DateTime? updated_at { get; set; }

        [BsonElement("helpful_votes"), BsonRepresentation(BsonType.Int32)]
        public int? helpful_votes { get; set; }

        [BsonElement("unhelpful_votes"), BsonRepresentation(BsonType.Int32)]
        public int? unhelpful_votes { get; set; }
    }
}

