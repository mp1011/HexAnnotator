using System.Linq;

namespace HexAnnotator.Models
{
    public class ByteRange
    {
        public AddressRange AddressRange { get; }

        public XByte[] Bytes { get; }

        public ByteRange(AddressRange addressRange, XByte[] bytes)
        {
            AddressRange = addressRange;
            Bytes = bytes;
        }

        public ByteRange(XByte[] bytes) : this(new AddressRange(0, bytes.Length - 1), bytes) { }


        public ByteRange Extract(AddressRange subRange)
        {
            return new ByteRange(subRange,
                Bytes.Where(b => subRange.Contains(b.Address))
               .ToArray());    
        }
    }
}
