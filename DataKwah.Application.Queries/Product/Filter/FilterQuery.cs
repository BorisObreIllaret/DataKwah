using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataKwah.Persistence.Repositories.Product;
using MediatR;

namespace DataKwah.Application.Queries.Product.Filter
{
    public class FilterQuery : IRequestHandler<FilterRequest, FilterResponse>
    {
        public FilterQuery(IProductRepository productRepository, IMapper mapper)
        {
            ProductRepository = productRepository;
            Mapper = mapper;
        }

        private IProductRepository ProductRepository { get; }
        private IMapper Mapper { get; }

        public async Task<FilterResponse> Handle(FilterRequest request, CancellationToken cancellationToken)
        {
            var queryObject = Mapper.Map<ProductQueryObject>(request);
            queryObject.IncludeReviews = true;
            queryObject.IncludeState = true;
            var (products, count) = await ProductRepository.FilterProducts(queryObject, cancellationToken);

            return new FilterResponse
            {
                Count = count,
                Items = Mapper.Map<List<FilterResponseData>>(products)
            };
        }
    }
}
