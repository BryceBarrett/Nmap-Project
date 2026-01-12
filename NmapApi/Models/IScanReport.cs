namespace NmapApi.Models
{
    public interface IScanReport
    {
        string HostName { get; set; }
        TimeSpan TimeBetweenScans { get; set; }
        List<Port> NewOpenPorts { get; set; }
        List<Port> NewClosedPorts { get; set; }
        string ErrorMessage { get; set; }
    }
}