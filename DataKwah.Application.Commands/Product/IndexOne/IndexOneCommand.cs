using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataKwah.Application.Services.Product;
using DataKwah.Domain.Entities;
using DataKwah.Persistence.Repositories.Product;
using MediatR;

namespace DataKwah.Application.Commands.Product.IndexOne
{
    public class IndexOneCommand : IRequestHandler<IndexOneRequest, IndexOneResponse>
    {
        public IndexOneCommand(IIndexationService indexationService, IProductRepository productRepository, IMapper mapper)
        {
            IndexationService = indexationService;
            ProductRepository = productRepository;
            Mapper = mapper;
        }

        private IIndexationService IndexationService { get; }
        private IProductRepository ProductRepository { get; }
        private IMapper Mapper { get; }

        public async Task<IndexOneResponse> Handle(IndexOneRequest request, CancellationToken cancellationToken = default)
        {
            Domain.Entities.Product product;

            if (await ProductRepository.IsAnyProductByAsin(request.Asin, cancellationToken))
            {
                product = await ProductRepository.GetProductByAsin(request.Asin, cancellationToken);
            }
            else
            {
                product = new Domain.Entities.Product
                {
                    Asin = request.Asin,
                    ProductState = new ProductState(),
                    Reviews = new List<Review>()
                };

                await ProductRepository.Add(product, cancellationToken);
            }

            await IndexationService.IndexAsin(request.Asin, product, cancellationToken);
            await ProductRepository.SaveChanges(cancellationToken);

            return Mapper.Map<IndexOneResponse>(product);
        }
    }
}
