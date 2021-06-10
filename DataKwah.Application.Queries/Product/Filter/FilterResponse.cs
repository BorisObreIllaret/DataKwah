using System.Collections.Generic;
using DataKwah.Core.Filter;

namespace DataKwah.Application.Queries.Product.Filter
{
    public class FilterResponse : IFilterResponse<FilterResponseData>
    {
        public IEnumerable<FilterResponseData> Items { get; set; }
        public int Count { get; set; }
    }

    public class FilterResponseData
    {
        public int Id { get; set; }
        public string Asin { get; set; }
        public string Label { get; set; }
        public byte State { get; set; }
    }
}
