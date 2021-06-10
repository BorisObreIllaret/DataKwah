using System.Threading;
using System.Threading.Tasks;
using DataKwah.Application.Commands.Product.IndexMany;
using DataKwah.Application.Commands.Product.IndexOne;
using DataKwah.Application.Queries.Product.Filter;
using Microsoft.AspNetCore.Mvc;

namespace DataKwah.Api.Controllers.Product
{
    public class ProductController : BaseController
    {
        [HttpPost("index-one")]
        public async Task<IndexOneResponse> IndexOneAsin(IndexOneRequest request, CancellationToken cancellationToken = default)
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpPost("index-many")]
        public async Task<IndexManyResponse> IndexManyAsins(IndexManyRequest request, CancellationToken cancellationToken = default)
        {
            return await Mediator.Send(request, cancellationToken);
        }

        [HttpGet("filter")]
        public async Task<FilterResponse> Filter([FromQuery] FilterRequest request, CancellationToken cancellationToken = default)
        {
            return await Mediator.Send(request, cancellationToken);
        }
    }
}
