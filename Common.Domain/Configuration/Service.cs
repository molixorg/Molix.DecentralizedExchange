using System;
using System.Collections.Generic;

namespace Common.Domain
{
    public class Service
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public IEnumerable<ConfigParam> Params { get; set; }
    }
}
