using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataKwah.Persistence.Repositories.Product;
using MediatR;

namespace DataKwah.Application.Queries.Product.Details
{
    public class DetailsQuery : IRequestHandler<DetailsRequest, DetailsResponse>
    {
        public DetailsQuery(IReviewRepository reviewRepository, IMapper mapper)
        {
            ReviewRepository = reviewRepository;
            Mapper = mapper;
        }

        private IReviewRepository ReviewRepository { get; }
        private IMapper Mapper { get; }

        public async Task<DetailsResponse> Handle(DetailsRequest request, CancellationToken cancellationToken)
        {
            var queryObject = Mapper.Map<ReviewQueryObject>(request);
            queryObject.IncludeProduct = true;
            var (reviews, count) = await ReviewRepository.FilterReviews(queryObject, cancellationToken);

            return new DetailsResponse
            {
                Count = count,
                Items = Mapper.Map<List<DetailsResponseData>>(reviews),
                ProductLabel = reviews.FirstOrDefault()?.Product.Label
            };
        }
    }
}