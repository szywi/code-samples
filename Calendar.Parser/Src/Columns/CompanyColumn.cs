using System;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Domain.Calendar.Parser.Columns.Base;
using Domain.Calendar.Parser.Columns.Exceptions;
using Domain.Calendar.Parser.Dto;

namespace Domain.Calendar.Parser.Columns
{
    public class CompanyColumn : BaseColumn<(string companyName, string symbol, decimal? marketCap)>
    {
        private const string BillionPostfix = "B";
        private const string MillionPostfix = "M";

        private readonly Regex symbolRegex = new Regex(@"\(([^)]+)\)");

        public override void AddColumnData(EarningsCalendarDto rowData, HtmlNode column)
        {
            if (!this.HasValidData(column))
            {
                throw new ColumnParsingException(this, column);
            }

            var (companyName, symbol, marketCap) = this.GetValue(column);

            rowData.CompanyName = companyName;
            rowData.Symbol = symbol;
            rowData.MarketCap = marketCap;
        }

        protected override string GetRawData(HtmlNode column)
        {
            var aTag = column.ChildNodes.FindFirst("a");

            return aTag.InnerText;
        }

        protected override bool HasValidData(HtmlNode column)
        {
            var rawData = this.GetRawData(column);

            var hasPostfix = HasMarketCapPostfix(rawData);
            var hasParenthesis = rawData.IndexOf("(", StringComparison.InvariantCulture) > 0 || rawData.IndexOf(")", StringComparison.InvariantCulture) > 0;

            return hasParenthesis && hasPostfix;
        }

        /// <summary>
        ///     Parses company column with format "Company full name (SYMBOL) MarketCap: value"
        /// </summary>
        protected override (string companyName, string symbol, decimal? marketCap) ParseValue(string rawData)
        {
            var symbolMatch = this.symbolRegex.Match(rawData);
            var symbolWithParenthesis = symbolMatch.Value;

            var symbol = symbolWithParenthesis.Replace("(", string.Empty).Replace(")", string.Empty);

            var indexOfSymbol = rawData.IndexOf(symbolWithParenthesis, StringComparison.InvariantCulture);
            var indexOfMarketCap = rawData.IndexOf("$", StringComparison.InvariantCulture);

            var company = rawData.Substring(0, indexOfSymbol - 1);

            var rawMarketCap = rawData.Substring(indexOfMarketCap + 1);
            var marketCap = ParseMarketCap(rawMarketCap);

            return (company, symbol, marketCap);
        }

        private static decimal? ParseMarketCap(string rawData)
        {
            decimal coefficient;
            string rawMarketCap;

            if (rawData.EndsWith(NotAvailable))
            {
                return null;
            }

            if (rawData.EndsWith(BillionPostfix))
            {
                coefficient = 1000000000m;
                rawMarketCap = rawData.Replace(BillionPostfix, string.Empty);
            }
            else
            {
                coefficient = 1000000m;
                rawMarketCap = rawData.Replace(MillionPostfix, string.Empty);
            }

            var marketCap = decimal.Parse(rawMarketCap) * coefficient;

            return marketCap;
        }

        private static bool HasMarketCapPostfix(string rawData)
        {
            return rawData.EndsWith(BillionPostfix) || rawData.EndsWith(MillionPostfix) || rawData.EndsWith(NotAvailable);
        }
    }
}