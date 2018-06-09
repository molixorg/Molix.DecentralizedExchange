using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string HashPassword { get; set; }
        public string HashAlgorithm { get; set; }
    }
}
