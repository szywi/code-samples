using System;
using Domain.Calendar.Parser.Enums;

namespace Domain.Calendar.Parser.Dto
{
    public class EarningsCalendarDto
    {
        public EarningsAnnounceTimeEnum AnnounceTime { get; set; }

        public string CompanyName { get; set; }

        public string Symbol { get; set; }

        public decimal? MarketCap { get; set; }

        public DateTime ReportDate { get; set; }

        public decimal? ConsensusEPS { get; set; }

        public int? NumberOfEstimates { get; set; }

        public DateTime? LastYearReportDate { get; set; }

        public decimal? LastYearEPS { get; set; }
    }
}