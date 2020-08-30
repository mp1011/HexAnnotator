using HexAnnotator.Extensions;
using HexAnnotator.Models;
using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace HexAnnotator.Converters
{
    public class ByteToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var byteView = parameter.ToString().ParseEnum<ByteView>();

            byte b = (byte)value;

            switch (byteView)
            {
                case ByteView.Decimal: return b.ToString();
                case ByteView.Ascii: return ((char)b).ToString();
                case ByteView.Hex: return b.ToString("X2");
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var byteView = parameter.ToString().ParseEnum<ByteView>();

            string str = value.ToString();
            byte b = 0;

            switch (byteView)
            {
                case ByteView.Decimal:
                    byte.TryParse(str, out b);
                    break;
                case ByteView.Ascii:
                    char c = str[0];
                    b = (byte)c;
                    break;
                case ByteView.Hex:
                    byte.TryParse(str, NumberStyles.HexNumber, null, out b);
                    break;
            }

            return b;
        }
    }
}
