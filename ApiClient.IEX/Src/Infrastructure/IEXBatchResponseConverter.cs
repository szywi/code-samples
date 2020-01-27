using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Domain.ApiClient.IEX.Dto;

namespace Domain.ApiClient.IEX.Infrastructure
{
    public class IEXBatchResponseConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var result = JObject.Load(reader);

            if (objectType == typeof(IEXCompaniesResponseDto))
            {
                return ConvertCompaniesResponseDto(result);
            }

            throw new InvalidOperationException($"Cannot convert type: {objectType}");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IEXCompaniesResponseDto);
        }

        private static object ConvertCompaniesResponseDto(JObject result)
        {
            var companies = new List<IEXCompanyDto>();

            foreach (var property in result.Properties())
            {
                var item = property.First["company"];

                var company = item.ToObject<IEXCompanyDto>(new JsonSerializer { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                companies.Add(company);
            }

            return new IEXCompaniesResponseDto { Companies = companies };
        }
    }
}