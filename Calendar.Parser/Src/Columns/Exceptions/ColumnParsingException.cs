using System;
using HtmlAgilityPack;
using Domain.Calendar.Parser.Columns.Base;

namespace Domain.Calendar.Parser.Columns.Exceptions
{
    public sealed class ColumnParsingException : Exception
    {
        public ColumnParsingException(IColumn column, HtmlNode columnData, Exception innerException = null)
            : base(CreateMessageForColumn(column), innerException)
        {
            this.Data["ColumnData"] = columnData.InnerHtml;
        }

        private static string CreateMessageForColumn(IColumn column)
        {
            return $"Parsing column {column.GetType()} failed";
        }
    }
}