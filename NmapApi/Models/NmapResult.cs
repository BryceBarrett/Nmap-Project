using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NmapApi.Models;

public class NmapResult
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string HostName { get; set; } = string.Empty;

    public string IpAddress { get; set; } = string.Empty;

    public List<int> OpenPorts { get; set; } = new List<int>();
}