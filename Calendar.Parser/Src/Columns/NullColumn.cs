using HtmlAgilityPack;
using Domain.Calendar.Parser.Columns.Base;
using Domain.Calendar.Parser.Dto;

namespace Domain.Calendar.Parser.Columns
{
    public class NullColumn : IColumn
    {
        public void AddColumnData(EarningsCalendarDto rowData, HtmlNode column)
        {
        }
    }
}