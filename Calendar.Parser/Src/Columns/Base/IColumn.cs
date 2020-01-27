using HtmlAgilityPack;
using Domain.Calendar.Parser.Dto;

namespace Domain.Calendar.Parser.Columns.Base
{
    public interface IColumn
    {
        void AddColumnData(EarningsCalendarDto rowData, HtmlNode column);
    }
}