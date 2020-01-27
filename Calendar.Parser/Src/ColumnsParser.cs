using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Domain.Calendar.Parser.Columns;
using Domain.Calendar.Parser.Columns.Base;
using Domain.Calendar.Parser.Dto;
using Domain.Calendar.Parser.Interfaces;

namespace Domain.Calendar.Parser.Services
{
    public class ColumnsParser : IColumnsParser
    {
        private readonly List<IColumn> columns;
        private readonly NullColumn nullColumn = new NullColumn();

        public ColumnsParser()
        {
            this.columns = new List<IColumn>
            {
                new TimeColumn(),
                new CompanyColumn(),
                new ReportDateColumn(),
                null, // fiscal quarter ending is not important
                new ConsensusEPSColumn(),
                new NumberOfEstimatesColumn(),
                new LastYearReportDateColumn(),
                new LastYearEPSColumn(),
                null // surprise column is always there it just have display: none
            };
        }

        public EarningsCalendarDto ParseColumns(HtmlNodeCollection cols)
        {
            var result = new EarningsCalendarDto();

            for (var i = 0; i < cols.Count; ++i)
            {
                var column = this.columns.ElementAt(i) ?? this.nullColumn;
                column.AddColumnData(result, cols[i]);
            }

            return result;
        }
    }
}