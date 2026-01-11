using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NmapApi.Models;

namespace NmapApi.Services;

public class NmapService
{
    private readonly IMongoCollection<NmapResult> _nmapCollection;

    public NmapService(
        IOptions<NmapDatabaseSettings> nmapDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            nmapDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            nmapDatabaseSettings.Value.DatabaseName);

        _nmapCollection = mongoDatabase.GetCollection<NmapResult>(
            nmapDatabaseSettings.Value.NmapCollectionName);
    }

    public async Task<List<NmapResult>> GetAsync() =>
        await _nmapCollection.Find(_ => true).ToListAsync();

    public async Task<NmapResult?> GetAsync(string id) =>
        await _nmapCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(NmapResult scanResult) =>
        await _nmapCollection.InsertOneAsync(scanResult);

    public async Task CreateAsync(List<NmapResult> scanResults) =>
        await _nmapCollection.InsertManyAsync(scanResults);
}