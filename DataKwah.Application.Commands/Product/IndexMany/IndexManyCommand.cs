using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataKwah.Application.Services.Product;
using DataKwah.Domain.Entities;
using DataKwah.Persistence.Repositories.Product;
using MediatR;

namespace DataKwah.Application.Commands.Product.IndexMany
{
    public class IndexManyCommand : IRequestHandler<IndexManyRequest, IndexManyResponse>
    {
        public IndexManyCommand(IIndexationService indexationService, IProductRepository productRepository)
        {
            IndexationService = indexationService;
            ProductRepository = productRepository;
        }

        private IIndexationService IndexationService { get; }
        private IProductRepository ProductRepository { get; }

        public async Task<IndexManyResponse> Handle(IndexManyRequest request, CancellationToken cancellationToken = default)
        {
            foreach (var asin in request.Asins)
            {
                Domain.Entities.Product product;

                if (await ProductRepository.IsAnyProductByAsin(asin, cancellationToken))
                {
                    product = await ProductRepository.GetProductByAsin(asin, cancellationToken);
                }
                else
                {
                    product = new Domain.Entities.Product
                    {
                        Asin = asin,
                        ProductState = new ProductState(),
                        Reviews = new List<Review>()
                    };

                    await ProductRepository.Add(product, cancellationToken);
                }

                await IndexationService.IndexAsin(asin, product, cancellationToken);
            }

            await ProductRepository.SaveChanges(cancellationToken);
            return new IndexManyResponse
            {
                IndexingCount = request.Asins.Count()
            };
        }
    }
}
