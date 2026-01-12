namespace NmapApi.Models
{
    public interface INmapDatabaseSettings
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

        string NmapCollectionName { get; set; }
    }
}
