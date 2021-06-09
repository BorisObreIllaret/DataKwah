using AutoMapper;

namespace DataKwah.Application.Commands.Product.IndexOne
{
    public class IndexOneMapper : Profile
    {
        public IndexOneMapper()
        {
            CreateMap<Domain.Entities.Product, IndexOneResponse>()
                .ForMember(dest => dest.State,
                    opt => opt.MapFrom(src => src.ProductState.State));
        }
    }
}
