using DataKwah.Core.Filter;

namespace DataKwah.Persistence.Repositories.Product
{
    public class ProductQueryObject : IFilterRequest
    {
        public bool IncludeReviews { get; set; }
        public bool IncludeState { get; set; }
        public bool UseWritable { get; set; }
        public int? Page { get; set; }
        public int? Limit { get; set; }
        public string Sort { get; set; }
        public bool AscendingOrder { get; set; }
        public string Search { get; set; }
    }
}
