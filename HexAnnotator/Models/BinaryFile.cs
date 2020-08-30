using System.Linq;

namespace HexAnnotator.Models
{
    public class BinaryFile
    {
        private Endian _endianness;
        public Endian Endianness
        {
            get => _endianness;
            set
            {
                _endianness = value;
                foreach (var b in Range.Bytes)
                    b.RaisePropertyChangeEvents();
            }
        }

        private ByteView _view;
        public ByteView View
        {
            get => _view;
            set
            {
                _view = value;
                foreach (var b in Range.Bytes)
                    b.RaisePropertyChangeEvents();
            }
        }

        public ByteRange Range { get; }

        public BinaryFile(Endian endianness, byte[] bytes)
        {
            var xBytes = bytes
                .Select((b,ix) => new XByte(this,ix, b))
                .ToArray();

            Range = new ByteRange(xBytes);

            View = ByteView.Decimal;
            Endianness = endianness;
        }
    }
}
