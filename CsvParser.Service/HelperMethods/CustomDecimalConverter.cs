using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;
using System.Globalization;


namespace CsvParser.Service.HelperMethods
{
    public class CustomDecimalConverter : DecimalConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return 0m;
            }

            text = text.Replace("$", "").Trim();

            if (decimal.TryParse(text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var decimalValue))
            {
                return decimalValue;
            }

            throw new TypeConverterException(this, memberMapData, text, row.Context, $"Cannot convert '{text}' to type decimal.");
        }
    }
}
