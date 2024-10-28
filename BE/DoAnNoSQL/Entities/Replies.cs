using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DoAnNoSQL.Entities
{
    public class Replies
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }

        [BsonElement("review_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? review_id { get; set; }

        [BsonElement("user_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? user_id { get; set; }

        [BsonElement("content"), BsonRepresentation(BsonType.String)]
        public string? content { get; set; }

        [BsonElement("created_at"), BsonRepresentation(BsonType.DateTime)]
        public DateTime created_at { get; set; }

        [BsonElement("updated_at"), BsonRepresentation(BsonType.DateTime)]
        public DateTime? updated_at { get; set; }
    }
}
