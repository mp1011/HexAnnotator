using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexAnnotator.Extensions
{
    public static class StringExtensions
    {
        public static T ParseEnum<T>(this string text) where T:struct
        {
            return Enum.Parse<T>(text);
        }
    }
}
