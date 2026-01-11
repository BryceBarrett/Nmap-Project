namespace NmapApi.Models;

public class NmapDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string NmapCollectionName { get; set; } = null!;
}