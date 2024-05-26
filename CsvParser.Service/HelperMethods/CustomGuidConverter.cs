using CsvHelper.Configuration;
using CsvHelper;
using CsvHelper.TypeConversion;

namespace CsvParser.Service.HelperMethods
{
    public class CustomGuidConverter : GuidConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return Guid.NewGuid();
            }
            return base.ConvertFromString(text, row, memberMapData);
        }
    }
}
