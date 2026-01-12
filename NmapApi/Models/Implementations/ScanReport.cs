namespace NmapApi.Models
{
    public class ScanReport
    {
        public string HostName { get; set; }
        public NmapResult NewResult { get; set; }
        public string TimeBetweenScans { get; set; }
        public List<Port> NewOpenPorts { get; set; } = new List<Port>();
        public List<Port> NewClosedPorts { get; set; } = new List<Port>();
        public string ErrorMessage { get; set; }
    }
}