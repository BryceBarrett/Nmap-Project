using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NmapApi.Models;

public class NmapResult : INmapResult
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string HostName { get; set; }

    public DateTime ScanCompletedAt { get; set; }

    public string IpAddress { get; set; }

    public List<Port> OpenPorts { get; set; } = new List<Port>();
}