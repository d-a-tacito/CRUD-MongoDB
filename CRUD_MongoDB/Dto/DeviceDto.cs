using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CRUD_MongoDB.Models;

public class DeviceDto
{
    public string Type { get; set; }

    public bool IsWorking { get; set; }
    
    public string StationId { get; set; }
}