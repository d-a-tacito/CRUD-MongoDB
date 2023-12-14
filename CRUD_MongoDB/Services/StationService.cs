using CRUD_MongoDB.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CRUD_MongoDB.Services;

public class StationService
{
    private readonly IMongoCollection<Station> _stationsCollection;

    public StationService(IOptions<MetroStoreDatabaseSettings> metroStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            metroStoreDatabaseSettings.Value.ConnectionString);
        
        var mongoDatabase = mongoClient.GetDatabase(
            metroStoreDatabaseSettings.Value.DatabaseName);

        _stationsCollection = mongoDatabase.GetCollection<Station>(
            metroStoreDatabaseSettings.Value.LinesCollectionName);
    }
    
    public async Task<List<Station>> GetAsync() =>
        await _stationsCollection.Find(_ => true).ToListAsync();

    public async Task<Station?> GetAsync(int id) =>
        await _stationsCollection.Find(l => l.Id == id).FirstOrDefaultAsync();
    
    public async Task CreateAsync(Station newStation) =>
        await _stationsCollection.InsertOneAsync(newStation);
    
    public async Task UpdateAsync(int id, Station updatedStation) =>
        await _stationsCollection.ReplaceOneAsync(x => x.Id == id, updatedStation);
    
    public async Task RemoveAsync(int id) =>
        await _stationsCollection.DeleteOneAsync(x => x.Id == id);
}