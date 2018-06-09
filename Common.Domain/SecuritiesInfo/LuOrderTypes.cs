using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public static class LuOrderTypes
    {
        public static char Buy = 'B';
        public static char Sell = 'S';
        public static char Undefined = 'U';
    }
}
