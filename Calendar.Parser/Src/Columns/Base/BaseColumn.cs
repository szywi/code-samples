using System;
using HtmlAgilityPack;
using Domain.Calendar.Parser.Columns.Exceptions;
using Domain.Calendar.Parser.Dto;

namespace Domain.Calendar.Parser.Columns.Base
{
    public abstract class BaseColumn<TResult> : IColumn
        where TResult : struct
    {
        protected const string NotAvailable = "n/a";

        public abstract void AddColumnData(EarningsCalendarDto rowData, HtmlNode column);

        protected abstract TResult ParseValue(string rawData);

        protected TResult GetValue(HtmlNode column)
        {
            try
            {
                var rawData = this.GetRawData(column);
                return this.ParseValue(rawData);
            }
            catch (Exception e)
            {
                throw new ColumnParsingException(this, column, e);
            }
        }

        protected TResult? GetValueOrDefault(HtmlNode column)
        {
            return !this.HasValidData(column) ? this.GetDefaultValue() : this.GetValue(column);
        }

        protected virtual TResult? GetDefaultValue()
        {
            return null;
        }

        protected virtual string GetRawData(HtmlNode column)
        {
            return column.FirstChild.InnerText;
        }

        protected virtual bool HasValidData(HtmlNode column)
        {
            var rawData = this.GetRawData(column);

            return rawData.IndexOf(NotAvailable, StringComparison.InvariantCultureIgnoreCase) == -1;
        }
    }
}