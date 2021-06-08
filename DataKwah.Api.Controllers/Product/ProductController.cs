using System.Threading;
using System.Threading.Tasks;
using DataKwah.Application.Commands.Product.IndexOne;
using Microsoft.AspNetCore.Mvc;

namespace DataKwah.Api.Controllers.Product
{
    public class ProductController : BaseController
    {
        [HttpPost("index")]
        public async Task<IndexOneResponse> IndexOneAsin(IndexOneRequest request, CancellationToken cancellationToken = default)
        {
            return await Mediator.Send(request, cancellationToken);
        }
    }
}