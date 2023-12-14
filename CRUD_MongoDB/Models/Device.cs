using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CRUD_MongoDB.Models;

public class Device
{
    [BsonId]
    [BsonRepresentation(BsonType.Int32)]
    public int Id { get; set; }

    [BsonElement("type")]
    public string Type { get; set; }

    [BsonElement("isWorking")]
    public bool IsWorking { get; set; }
    
    [BsonElement("station_id")]
    public int StationId { get; set; }
}