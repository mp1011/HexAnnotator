using System;
using System.Linq;

namespace HexAnnotator.Extensions
{
    public static class ByteExtensions
    {
        public static byte Invert(this byte b)
        {
            var bin = Convert.ToString(b, 2).PadLeft(8, '0');
            var revBin = new string(bin.Reverse().ToArray());

            var result = Convert.ToByte(revBin, 2);
            return result;
        }
    }
}
