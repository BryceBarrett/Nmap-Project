using NmapApi.Models;
using System.Xml.Linq;

public static class XmlParser
{
    public static List<NmapResult> ParseXmlString(string xmlString, string hostName)
    {
        // String given to us from nmap xml output
        XElement results = XElement.Parse(xmlString);

        // Builds from our XML nmap output. Grabs the ip address from the host element,
        // and builds out the port information
        var hosts = from host in results.Descendants("host")
                    let ipAddr = host.Element("address")?.Attribute("addr")?.Value
                    select new NmapResult
                    {
                        IpAddress = ipAddr,
                        HostName = hostName,
                        ScanCompletedAt = DateTime.Now.ToUniversalTime(),
                        OpenPorts = [.. (from port in results.Descendants("port")
                                    let state = port.Element("state")?.Attribute("state")?.Value
                                    let service = port.Element("service")?.Attribute("name")?.Value
                                    select new Port
                                    {
                                        PortId = (int)port.Attribute("portid"),
                                        Protocol = port.Attribute("protocol")?.Value,
                                        IsOpen = state == "open" ? true : false,
                                        ServiceName = service
                                    })]
                    };

        // We only want to return the single element that will be in the list
        return hosts.ToList();
    }
}