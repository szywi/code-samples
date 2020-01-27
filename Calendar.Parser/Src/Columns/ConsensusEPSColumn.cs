﻿using HtmlAgilityPack;
using Domain.Calendar.Parser.Columns.Base;
using Domain.Calendar.Parser.Columns.Helpers;
using Domain.Calendar.Parser.Dto;

namespace Domain.Calendar.Parser.Columns
{
    public class ConsensusEPSColumn : BaseColumn<decimal>
    {
        public override void AddColumnData(EarningsCalendarDto rowData, HtmlNode column)
        {
            rowData.ConsensusEPS = this.GetValueOrDefault(column);
        }

        protected override decimal ParseValue(string rawData)
        {
            return ColumnParsingHelper.ParsePrice(rawData);
        }
    }
}