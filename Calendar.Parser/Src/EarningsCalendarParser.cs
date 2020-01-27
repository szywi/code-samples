using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Domain.Calendar.Parser.Columns.Exceptions;
using Domain.Calendar.Parser.Dto;
using Domain.Calendar.Parser.Interfaces;

namespace Domain.Calendar.Parser.Services
{
    public class EarningsCalendarParser : IParseEarningsCalendar
    {
        private readonly IColumnsParser columnsParser;

        public EarningsCalendarParser(IColumnsParser columnsParser)
        {
            this.columnsParser = columnsParser;
        }

        public IEnumerable<EarningsCalendarDto> ParseEarningsCalendar(string rawHtml)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(rawHtml);

            var rows = htmlDocument.DocumentNode
                .SelectNodes("//table[@id='ECCompaniesTable']//tr");

            if (rows.Count == 0)
            {
                return Enumerable.Empty<EarningsCalendarDto>();
            }

            var rowsWithoutTableHeader = rows.Skip(1);

            var results = new List<EarningsCalendarDto>();
            foreach (var row in rowsWithoutTableHeader)
            {
                var cols = htmlDocument.DocumentNode.SelectNodes($"{row.XPath}//td");

                try
                {
                    var result = this.columnsParser.ParseColumns(cols);
                    results.Add(result);
                }
                catch (ColumnParsingException)
                {
                    // TODO: Add logging of failed rows
                }
            }

            return results;
        }
    }
}