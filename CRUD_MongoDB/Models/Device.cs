using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CRUD_MongoDB.Models;

public class Device
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonIgnoreIfDefault]
    public string? Id { get; set; }

    [BsonElement("type")]
    public string Type { get; set; }

    [BsonElement("isWorking")]
    public bool IsWorking { get; set; }
    
    [BsonElement("stationId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string StationId { get; set; }
}