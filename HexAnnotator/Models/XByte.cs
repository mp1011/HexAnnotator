using HexAnnotator.Extensions;
using System.ComponentModel;

namespace HexAnnotator.Models
{
    public class XByte : INotifyPropertyChanged
    {
        private BinaryFile _file;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Address { get; }

        public Endian Endianness
        {
            get => _file.Endianness;
            set => _file.Endianness = value;
        }

        public ByteView View
        {
            get => _file.View;
            set => _file.View = value;
        }

        public void RaisePropertyChangeEvents()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Endianness)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(View)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BigEndianValue)));

        }

        public byte Value { get; private set; }


        public byte BigEndianValue
        {
            get
            {
                if (Endianness == Endian.Big || Value == 0 || Value==255)
                    return Value;
                else
                    return Value.Invert();               
            }
            set
            {
                if (Endianness == Endian.Big || Value == 0 || Value == 255)
                    Value = value;
                else
                    Value = value.Invert();
            }
        }

        public XByte(BinaryFile file, int address, byte value)
        {
            _file = file;
            Value = value;
            Address = address;
        }
    }
}
