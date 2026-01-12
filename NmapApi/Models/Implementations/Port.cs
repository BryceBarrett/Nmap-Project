namespace NmapApi.Models
{
    public class Port : IPort
    {
        public int PortId { get; set; }
        public string Protocol { get; set; }
        public bool IsOpen { get; set; }
        public string ServiceName { get; set; }
    }
}
