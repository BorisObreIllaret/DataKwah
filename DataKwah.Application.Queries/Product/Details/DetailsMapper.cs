using AutoMapper;
using DataKwah.Domain.Entities;
using DataKwah.Persistence.Repositories.Product;

namespace DataKwah.Application.Queries.Product.Details
{
    public class DetailsMapper : Profile
    {
        public DetailsMapper()
        {
            CreateMap<DetailsRequest, ReviewQueryObject>();

            CreateMap<Review, DetailsResponseData>();
        }
    }
}