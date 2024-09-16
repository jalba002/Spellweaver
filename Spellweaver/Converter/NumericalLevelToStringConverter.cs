using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Spellweaver.Converter
{
    public class NumericalLevelToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cleanString = value as string;
            if (cleanString != null)
            {
                Regex rgx = new Regex("[^a-zA-Z -]");
                cleanString = rgx.Replace(cleanString, "");
                _ = int.TryParse(cleanString, out var level);
                return level;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
