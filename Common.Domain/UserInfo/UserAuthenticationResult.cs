using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    public class UserAuthenticationResult
    {
        public Guid UserId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
