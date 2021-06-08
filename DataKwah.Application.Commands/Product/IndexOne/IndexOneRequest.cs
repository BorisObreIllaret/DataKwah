using MediatR;

namespace DataKwah.Application.Commands.Product.IndexOne
{
    public class IndexOneRequest : IRequest<IndexOneResponse>
    {
        public string Asin { get; set; }
    }
}