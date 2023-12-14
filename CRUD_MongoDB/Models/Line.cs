using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CRUD_MongoDB.Models;

public class Line
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }
}