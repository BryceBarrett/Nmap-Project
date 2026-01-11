using NmapApi.Models;
using System.Xml.Linq;

public class XmlParser
{
    public static List<NmapResult> ParseXmlString(string xmlString, string hostName)
    {
        XElement results = XElement.Parse(xmlString);

        var hosts = from host in results.Descendants("host")
                    select new NmapResult
                    {
                        IpAddress = host.Descendants("address")
                                        .FirstOrDefault(addr => (string)addr.Attribute("addrtype") == "ipv4")
                                        ?.Attribute("addr")?.Value,

                        OpenPorts = (from port in host.Descendants("port")
                                     let state = port.Element("state")
                                     where (string)state.Attribute("state") == "open"
                                     select (int)port.Attribute("portid"))
                                    .ToList(),

                        HostName = hostName
                    };

        return hosts.ToList();
    }
}