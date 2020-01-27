using System.Linq;
using HtmlAgilityPack;
using Domain.Calendar.Parser.Columns.Base;
using Domain.Calendar.Parser.Columns.Exceptions;
using Domain.Calendar.Parser.Dto;
using Domain.Calendar.Parser.Enums;

namespace Domain.Calendar.Parser.Columns
{
    public class TimeColumn : BaseColumn<EarningsAnnounceTimeEnum>
    {
        private const string AnnounceTimeAfterHours = "After Hours Quotes";
        private const string AnnounceTimePreMarket = "Pre-Market Quotes";

        public override void AddColumnData(EarningsCalendarDto rowData, HtmlNode column)
        {
            rowData.AnnounceTime = this.GetValue(column);
        }

        protected override string GetRawData(HtmlNode column)
        {
            var reportTime = column.ChildNodes.FindFirst("a");
            var altAttribute = reportTime?.FirstChild.Attributes.SingleOrDefault(x => x.Name == "alt");

            if (altAttribute != null)
            {
                return altAttribute.Value;
            }

            throw new ColumnParsingException(this, column);
        }

        protected override EarningsAnnounceTimeEnum ParseValue(string rawData)
        {
            switch (rawData)
            {
                case AnnounceTimeAfterHours:
                    return EarningsAnnounceTimeEnum.AfterClose;
                case AnnounceTimePreMarket:
                    return EarningsAnnounceTimeEnum.BeforeOpen;
                default:
                    return EarningsAnnounceTimeEnum.Unknown;
            }
        }
    }
}