using GalaSoft.MvvmLight;
using HexAnnotator.Models;
using HexAnnotator.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace HexAnnotator.ViewModels
{
    public class HexGridViewModel : ViewModelBase
    {
        private const int MaxRange = 700;
        private readonly FileReader _fileReader;
        private readonly BinaryFileService _binaryFileService;

        public HexGridViewModel(FileReader fileReader, BinaryFileService binaryFileService)
        {
            _fileReader = fileReader;
            _binaryFileService = binaryFileService;
        }

        public BinaryFile File { get; private set; }

        public ObservableCollection<XByte> Bytes { get; } = new ObservableCollection<XByte>();

        public ObservableCollection<ByteRange> Blocks { get; } = new ObservableCollection<ByteRange>();

        public ByteView ByteView 
        {
            get => File == null ? ByteView.Decimal : File.View;
            set 
            {
                if (File.View != value)
                {
                    File.View = value;

                    RaisePropertyChanged(nameof(DecimalView));
                    RaisePropertyChanged(nameof(HexView));
                    RaisePropertyChanged(nameof(AsciiView));
                }
            }
        }

        public bool DecimalView
        {
            get => ByteView == ByteView.Decimal;
            set => ByteView = ByteView.Decimal;
        }

        public bool HexView
        {
            get => ByteView == ByteView.Hex;
            set => ByteView = ByteView.Hex;
        }

        public bool AsciiView
        {
            get => ByteView == ByteView.Ascii;
            set => ByteView = ByteView.Ascii;
        }

        public bool IsLittleEndian
        {
            get => File != null && File.Endianness == Endian.Little;
            set
            {
                File.Endianness = value ? Endian.Little : Endian.Big;
                RaisePropertyChanged(nameof(IsLittleEndian));
            }
        }

        private AddressRange _range = new AddressRange(0, MaxRange - 1);

        public int RangeStart
        {
            get => _range.Start;
            set
            {
                if (_range.Start != value)
                {
                    _range = new AddressRange(value, value + _range.Length-1);

                    if (_range.Length > MaxRange)
                        _range = new AddressRange(_range.End-MaxRange, _range.End);

                    RaisePropertyChanged(nameof(RangeStart));
                    ExtractSelectedRange();
                }
            }
        }

        public int RangeEnd
        {
            get => _range.End;
            set
            {
                if (_range.End != value)
                {
                    _range = new AddressRange(value - _range.Length - 1, value);

                    if (_range.Length > MaxRange)
                        _range = new AddressRange(_range.Start, _range.Start + MaxRange-1);

                    RaisePropertyChanged(nameof(RangeStart));
                    ExtractSelectedRange();
                }
            }
        }

        public async Task LoadFile(string name)
        {
            File = await _fileReader.Read(name, Endian.Little);
            _range = new AddressRange(0, MaxRange-1);
            ExtractSelectedRange();

            Blocks.Clear();
            foreach (var block in _binaryFileService.SplitByRepeatingBytes(File, 255, 10))
                Blocks.Add(block);
        }

        public void ExtractSelectedRange()
        {
            Bytes.Clear();
            foreach (var b in File.Range.Extract(_range).Bytes)
                Bytes.Add(b);
        }
    }
}
