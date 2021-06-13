using System;
using System.Collections.Generic;
using DataKwah.Core.Filter;

namespace DataKwah.Application.Queries.Product.Details
{
    public class DetailsResponse : IFilterResponse<DetailsResponseData>
    {
        public string ProductLabel { get; set; }
        public IEnumerable<DetailsResponseData> Items { get; set; }
        public int Count { get; set; }
    }

    public class DetailsResponseData
    {
        public int Id { get; set; }
        public string Asin { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public byte? Rating { get; set; }
        public DateTime? Date { get; set; }
    }
}