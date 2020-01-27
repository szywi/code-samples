using System.Collections.Generic;
using Domain.Calendar.Parser.Dto;

namespace Domain.Calendar.Parser.Interfaces
{
    public interface IParseEarningsCalendar
    {
        IEnumerable<EarningsCalendarDto> ParseEarningsCalendar(string rawHtml);
    }
}