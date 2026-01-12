using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NmapApi.Models;

namespace NmapApi.Services;

public class NmapService : INmapService
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

    public async Task<List<NmapResult>> GetAllAsync() =>
        await _nmapCollection.Find(_ => true).ToListAsync();

    public async Task<List<NmapResult>> GetAsync(string hostName) =>
        await _nmapCollection.Find(x => x.HostName == hostName).ToListAsync();

    public async Task CreateAsync(NmapResult scanResult) =>
        await _nmapCollection.InsertOneAsync(scanResult);

    public async Task CreateAsync(List<NmapResult> scanResults) =>
        await _nmapCollection.InsertManyAsync(scanResults);

    public async Task<NmapResult?> GetMostRecentAsync(string hostName)
    {
        var sortRecent = Builders<NmapResult>.Sort.Descending(d => d.ScanCompletedAt);

        return await _nmapCollection.Find(x => x.HostName == hostName).Sort(sortRecent).FirstOrDefaultAsync();
    }

    public async Task<List<NmapResult>> GetResultsByHostName(string hostName)
    {
        return await _nmapCollection.Find(x => x.HostName == hostName).ToListAsync();
    }
}