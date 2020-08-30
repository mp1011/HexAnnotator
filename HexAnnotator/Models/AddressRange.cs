namespace HexAnnotator.Models
{
    public class AddressRange
    {
        /// <summary>
        /// Position of first byte
        /// </summary>
        public int Start { get; }

        /// <summary>
        /// Position of last byte
        /// </summary>
        public int End { get; }

        /// <summary>
        /// Number of bytes
        /// </summary>
        public int Length => (End - Start) + 1;

        public bool Contains(int address)
        {
            return address >= Start && address <= End;
        }

        public AddressRange(int start, int end)
        {
            Start = start;
            End = end;
        }
    }
}
