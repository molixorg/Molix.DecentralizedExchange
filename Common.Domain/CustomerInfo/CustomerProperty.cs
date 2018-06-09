using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class CustomerProperty
    {
        public Guid CustomerId { get; set; }
        public LuCustomerPropertyTypes CustomerPropertyType { get; set; }
        public int CustomerPropertyTypeOId
        {
            get
            {
                return (int)CustomerPropertyType;
            }
            set
            {
                CustomerPropertyType = (LuCustomerPropertyTypes)value;
            }
        }
        public string Value { get; set; }
    }
}
