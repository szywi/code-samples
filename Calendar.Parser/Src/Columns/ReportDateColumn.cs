using System;
using HtmlAgilityPack;
using Domain.Calendar.Parser.Columns.Base;
using Domain.Calendar.Parser.Columns.Helpers;
using Domain.Calendar.Parser.Dto;

namespace Domain.Calendar.Parser.Columns
{
    public class ReportDateColumn : BaseColumn<DateTime>
    {
        public override void AddColumnData(EarningsCalendarDto rowData, HtmlNode column)
        {
            rowData.ReportDate = this.GetValue(column);
        }

        protected override DateTime ParseValue(string rawData)
        {
            return ColumnParsingHelper.ParseDate(rawData);
        }
    }
}