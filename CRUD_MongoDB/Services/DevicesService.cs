using CRUD_MongoDB.Dto;
using CRUD_MongoDB.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CRUD_MongoDB.Services;

public class DevicesService
{
    private readonly IMongoCollection<Device> _devicesCollection;

    public DevicesService(IOptions<MetroStoreDatabaseSettings> metroStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            metroStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            metroStoreDatabaseSettings.Value.DatabaseName);

        _devicesCollection = mongoDatabase.GetCollection<Device>(
            metroStoreDatabaseSettings.Value.DevicesCollectionName);
    }

    public async Task<List<Device>> GetAsync() =>
        await _devicesCollection.Find(_ => true).ToListAsync();

    public async Task<Device?> GetAsync(string id) =>
        await _devicesCollection.Find(l => l.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Device newDevice) =>
        await _devicesCollection.InsertOneAsync(newDevice);

    public async Task UpdateAsync(string id, Device updatedDevice) =>
        await _devicesCollection.ReplaceOneAsync(x => x.Id == id, updatedDevice);

    public async Task RemoveAsync(string id) =>
        await _devicesCollection.DeleteOneAsync(x => x.Id == id);

    public async Task<IEnumerable<TypeCountDto>> CountDevicesByTypeAsync()
    {
        var group = new BsonDocument
        {
            { "_id", "$type" },
            { "count", new BsonDocument("$sum", 1) }
        };

        var aggregateFluent = _devicesCollection.Aggregate()
            .Group<BsonDocument>(group);

        var results = await aggregateFluent.ToListAsync();
        return results.Select(r => new TypeCountDto
        {
            Type = r["_id"].AsString,
            Count = r["count"].AsInt32
        });
    }
}