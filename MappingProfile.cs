using AutoMapper;
using educational_practice5.Models;
using educational_practice5.Services.DataTransferObjects;

namespace educational_practice5
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductForCreationDto, Product>();
            CreateMap<ProductForUpdateDto, Product>();
        }
    }
}   