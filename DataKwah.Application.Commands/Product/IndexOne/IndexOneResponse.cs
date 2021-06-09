using DataKwah.Domain.Entities;

namespace DataKwah.Application.Commands.Product.IndexOne
{
    public class IndexOneResponse
    {
        public string Asin { get; set; }
        public string Label { get; set; }
        public ProductIndexationState State { get; set; }
    }
}
