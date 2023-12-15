using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CRUD_MongoDB.Models;

public class Line
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonIgnoreIfDefault]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }
}