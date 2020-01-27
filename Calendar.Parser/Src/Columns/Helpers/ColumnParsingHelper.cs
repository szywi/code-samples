using System;
using System.Globalization;

namespace Domain.Calendar.Parser.Columns.Helpers
{
    public static class ColumnParsingHelper
    {
        public static DateTime ParseDate(string rawDate)
        {
            var date = DateTime.ParseExact(rawDate.Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

            return date;
        }

        public static decimal ParsePrice(string rawData)
        {
            var rawDecimal = rawData.Trim().Replace("$", string.Empty);

            return decimal.Parse(rawDecimal);
        }
    }
}