using DataKwah.Core.Filter;
using MediatR;

namespace DataKwah.Application.Queries.Product.Filter
{
    public class FilterRequest : IRequest<FilterResponse>, IFilterRequest
    {
        public int? Page { get; set; }
        public int? Limit { get; set; }
        public string Sort { get; set; }
        public bool AscendingOrder { get; set; }
        public string Search { get; set; }
    }
}
