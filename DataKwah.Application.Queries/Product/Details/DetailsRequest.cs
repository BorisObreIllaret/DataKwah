using DataKwah.Core.Filter;
using MediatR;

namespace DataKwah.Application.Queries.Product.Details
{
    public class DetailsRequest : IRequest<DetailsResponse>, IFilterRequest
    {
        public int ProductId { get; set; }
        public int? Page { get; set; }
        public int? Limit { get; set; }
        public string Sort { get; set; }
        public bool AscendingOrder { get; set; }
        public string Search { get; set; }
    }
}