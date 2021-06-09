using System.Collections.Generic;
using MediatR;

namespace DataKwah.Application.Commands.Product.IndexMany
{
    public class IndexManyRequest : IRequest<IndexManyResponse>
    {
        public IEnumerable<string> Asins { get; set; } = new List<string>();
    }
}
