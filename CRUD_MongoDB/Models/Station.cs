using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CRUD_MongoDB.Models;

public class Station
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonIgnoreIfDefault]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("lineId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string LineId { get; set; }
}