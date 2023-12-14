using CRUD_MongoDB.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CRUD_MongoDB.Services;

public class DeviceService
{
    private readonly IMongoCollection<Device> _devicessCollection;

    public DeviceService(IOptions<MetroStoreDatabaseSettings> metroStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            metroStoreDatabaseSettings.Value.ConnectionString);
        
        var mongoDatabase = mongoClient.GetDatabase(
            metroStoreDatabaseSettings.Value.DatabaseName);

        _devicessCollection = mongoDatabase.GetCollection<Device>(
            metroStoreDatabaseSettings.Value.DevicesCollectionName);
    }
    
    public async Task<List<Device>> GetAsync() =>
        await _devicessCollection.Find(_ => true).ToListAsync();

    public async Task<Device?> GetAsync(int id) =>
        await _devicessCollection.Find(l => l.Id == id).FirstOrDefaultAsync();
    
    public async Task CreateAsync(Device newDevice) =>
        await _devicessCollection.InsertOneAsync(newDevice);
    
    public async Task UpdateAsync(int id, Device updatedDevice) =>
        await _devicessCollection.ReplaceOneAsync(x => x.Id == id, updatedDevice);
    
    public async Task RemoveAsync(int id) =>
        await _devicessCollection.DeleteOneAsync(x => x.Id == id);
}