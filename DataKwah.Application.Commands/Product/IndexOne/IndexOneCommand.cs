using System.Threading;
using System.Threading.Tasks;
using DataKwah.Application.Services.Product;
using MediatR;

namespace DataKwah.Application.Commands.Product.IndexOne
{
    public class IndexOneCommand : IRequestHandler<IndexOneRequest, IndexOneResponse>
    {
        public IndexOneCommand(IIndexationService indexationService)
        {
            IndexationService = indexationService;
        }

        private IIndexationService IndexationService { get; }

        public async Task<IndexOneResponse> Handle(IndexOneRequest request, CancellationToken cancellationToken)
        {
            await IndexationService.IndexAsin(request.Asin, cancellationToken);
            return new IndexOneResponse();
        }
    }
}