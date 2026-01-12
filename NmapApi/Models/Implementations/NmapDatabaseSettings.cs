namespace NmapApi.Models;

public class NmapDatabaseSettings : INmapDatabaseSettings
{
    public string ConnectionString { get; set; }

    public string DatabaseName { get; set; }

    public string NmapCollectionName { get; set; }
}