﻿using CRUD_MongoDB.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CRUD_MongoDB.Services;

public class LinesService
{
    private readonly IMongoCollection<Line> _linesCollection;

    public LinesService(IOptions<MetroStoreDatabaseSettings> metroStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            metroStoreDatabaseSettings.Value.ConnectionString);
        
        var mongoDatabase = mongoClient.GetDatabase(
            metroStoreDatabaseSettings.Value.DatabaseName);

        _linesCollection = mongoDatabase.GetCollection<Line>(
            metroStoreDatabaseSettings.Value.LinesCollectionName);
    }
    
    public async Task<List<Line>> GetAsync() =>
        await _linesCollection.Find(_ => true).ToListAsync();

    public async Task<Line?> GetAsync(string id) =>
        await _linesCollection.Find(l => l.Id == id).FirstOrDefaultAsync();
    
    public async Task CreateAsync(Line newLine) =>
        await _linesCollection.InsertOneAsync(newLine);
    
    public async Task UpdateAsync(string id, Line updatedLine) =>
        await _linesCollection.ReplaceOneAsync(x => x.Id == id, updatedLine);
    
    public async Task RemoveAsync(string id) =>
        await _linesCollection.DeleteOneAsync(x => x.Id == id);
}