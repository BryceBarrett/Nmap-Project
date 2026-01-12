namespace NmapApi.Models
{
    public interface IPort
    {
        int PortId { get; set; }
        string Protocol { get; set; }
        bool IsOpen { get; set; }
        string ServiceName { get; set; }
    }
}