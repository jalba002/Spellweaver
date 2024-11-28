using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Spellweaver.Converter
{
    [ValueConversion(typeof(IEnumerable<bool>), typeof(string))]
    public class RitualConcentrationConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var isRitual = values[0];
            var isConcentration = values[1];

            string result = (bool)isRitual ? "Ritual" : string.Empty;
            string concResult = (bool)isConcentration ? "Concentration" : string.Empty;

            string finalResult = string.Join(" & ", new[] { result, concResult });
            return finalResult;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}