using System;
using System.Linq;

namespace HexAnnotator.Models
{
    public class ByteRange
    {
        public AddressRange AddressRange { get; }

        public int Length => AddressRange.Length;

        public XByte[] Bytes { get; }

        public ByteRange(AddressRange addressRange, XByte[] bytes)
        {
            AddressRange = addressRange;
            Bytes = bytes;
        }

        public ByteRange(XByte[] bytes) : this(new AddressRange(0, bytes.Length - 1), bytes) { }


        public ByteRange Extract(int firstByteAddress, int lastByteAddress)
        {
            return Extract(new AddressRange(firstByteAddress, lastByteAddress));
        }
         
        public ByteRange Extract(AddressRange subRange)
        {
            return new ByteRange(subRange,
                Bytes.Where(b => subRange.Contains(b.Address))
               .ToArray());    
        }

        public XByte Find(int start, byte[] search)
        {
            int matchCounter = 0;

            XByte maybeMatch = null;

            for (int index = start; index < Length; index++)
            {
                if (Bytes[index].BigEndianValue != search[matchCounter])
                {
                    matchCounter = 0;
                    maybeMatch = null;
                }
                else
                {
                    maybeMatch = maybeMatch ?? Bytes[index];
                    matchCounter++;
                    if (matchCounter == search.Length)
                        return maybeMatch;
                }
            }

            return null;
        }

        public XByte Find(int start, Func<XByte,bool> condition)
        {
            int index = start;
            while(index < Length)
            {
                if (condition(Bytes[index]))
                    return Bytes[index];
                index++;
            }

            return null;
        }
    }
}
