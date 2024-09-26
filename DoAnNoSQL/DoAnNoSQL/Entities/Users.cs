using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DoAnNoSQL.Entities
{
    public class Users
    {
        [BsonId]
        [BsonElement("_id"),BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonElement("username"), BsonRepresentation(BsonType.String)]
        public string? username { get; set; }
        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        public string? email { get; set; }
        [BsonElement("password"), BsonRepresentation(BsonType.String)]
        public string? password { get; set; }
        [BsonElement("avatar"),BsonRepresentation(BsonType.String)]
        public string? avatar { get; set; }
        [BsonElement("roles")]
        public string[]? roles { get; set; }
    }
}