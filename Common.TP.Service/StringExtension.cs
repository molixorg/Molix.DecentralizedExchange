using Common.Domain;
using System;

namespace Common.TP.Service
{
    public static class StringExtension
    {
        public static char ToOrderTypeChar(this string source)
        {
            if ("buy".Equals(source, StringComparison.OrdinalIgnoreCase) ||
                "bid".Equals(source, StringComparison.OrdinalIgnoreCase) ||
                "b".Equals(source, StringComparison.OrdinalIgnoreCase))
            {
                return LuOrderTypes.Buy;
            }else if ("sell".Equals(source, StringComparison.OrdinalIgnoreCase) ||
                "ask".Equals(source, StringComparison.OrdinalIgnoreCase) ||
                "s".Equals(source, StringComparison.OrdinalIgnoreCase))
            {
                return LuOrderTypes.Sell;
            }
            else
            {
                return LuOrderTypes.Undefined;
            }
        }
    }
}
