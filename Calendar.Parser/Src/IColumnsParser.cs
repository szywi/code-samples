using HtmlAgilityPack;
using Domain.Calendar.Parser.Dto;

namespace Domain.Calendar.Parser.Interfaces
{
    public interface IColumnsParser
    {
        EarningsCalendarDto ParseColumns(HtmlNodeCollection cols);
    }
}