using CRUD_MongoDB.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CRUD_MongoDB.Services;

public class StationsService
{
    private readonly IMongoCollection<Station> _stationsCollection;

    public StationsService(IOptions<MetroStoreDatabaseSettings> metroStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            metroStoreDatabaseSettings.Value.ConnectionString);
        
        var mongoDatabase = mongoClient.GetDatabase(
            metroStoreDatabaseSettings.Value.DatabaseName);

        _stationsCollection = mongoDatabase.GetCollection<Station>(
            metroStoreDatabaseSettings.Value.StationsCollectionName);
    }
    
    public async Task<List<Station>> GetAsync() =>
        await _stationsCollection.Find(_ => true).ToListAsync();

    public async Task<Station?> GetAsync(string id) =>
        await _stationsCollection.Find(l => l.Id == id).FirstOrDefaultAsync();
    
    public async Task CreateAsync(Station newStation) =>
        await _stationsCollection.InsertOneAsync(newStation);
    
    public async Task UpdateAsync(string id, Station updatedStation) =>
        await _stationsCollection.ReplaceOneAsync(x => x.Id == id, updatedStation);
    
    public async Task RemoveAsync(string id) =>
        await _stationsCollection.DeleteOneAsync(x => x.Id == id);
}