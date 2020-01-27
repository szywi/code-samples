using System;

namespace Domain.ApiClient.IEX.Dto
{
    public class IEXCompanyDto
    {
        public string Symbol { get; set; }

        public string CompanyName { get; set; }

        public string Exchange { get; set; }

        public string Industry { get; set; }

        public string Website { get; set; }

        public string Description { get; set; }

        public string CEO { get; set; }

        public string Sector { get; set; }
    }
}