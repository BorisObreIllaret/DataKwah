using AutoMapper;
using DataKwah.Persistence.Repositories.Product;

namespace DataKwah.Application.Queries.Product.Filter
{
    public class FilterMapper : Profile
    {
        public FilterMapper()
        {
            CreateMap<FilterRequest, ProductQueryObject>();

            CreateMap<Domain.Entities.Product, FilterResponseData>()
                .ForMember(dest => dest.State,
                    opt => opt.MapFrom(src => src.ProductState.StateId));
        }
    }
}
