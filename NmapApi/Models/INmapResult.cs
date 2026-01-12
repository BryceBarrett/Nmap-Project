namespace NmapApi.Models
{
    public interface INmapResult
    {
        string? Id { get; set; }

        string HostName { get; set; }

        DateTime ScanCompletedAt { get; set; }

        string IpAddress { get; set; }

        List<Port> OpenPorts { get; set; }
    }
}