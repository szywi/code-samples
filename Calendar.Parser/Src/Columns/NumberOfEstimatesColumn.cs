using HtmlAgilityPack;
using Domain.Calendar.Parser.Columns.Base;
using Domain.Calendar.Parser.Dto;

namespace Domain.Calendar.Parser.Columns
{
    public class NumberOfEstimatesColumn : BaseColumn<int>
    {
        public override void AddColumnData(EarningsCalendarDto rowData, HtmlNode column)
        {
            rowData.NumberOfEstimates = this.GetValueOrDefault(column);
        }

        protected override int ParseValue(string rawData)
        {
            string rawInt = rawData.Trim();

            return int.Parse(rawInt);
        }
    }
}