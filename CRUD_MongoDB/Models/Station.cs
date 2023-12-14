using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CRUD_MongoDB.Models;

public class Station
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("line_id")]
    public int LineId { get; set; }
}