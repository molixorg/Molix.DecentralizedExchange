using System.Collections.Generic;

namespace Common.Domain
{
    public class ServiceType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ConfigParam> Params { get; set; }
        public IList<Service> Services { get; set; }
    }
}
