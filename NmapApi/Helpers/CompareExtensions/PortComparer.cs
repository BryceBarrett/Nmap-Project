using NmapApi.Models;

namespace NmapApi.Helpers.CompareExtensions
{
    public class PortComparer : IEqualityComparer<Port>
    {
        public bool Equals(Port x, Port y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.PortId == y.PortId && x.Protocol == y.Protocol && x.ServiceName == y.ServiceName;
        }

        public int GetHashCode(Port obj)
        {
            return HashCode.Combine(obj.PortId, obj.Protocol, obj.ServiceName);
        }
    }
}
