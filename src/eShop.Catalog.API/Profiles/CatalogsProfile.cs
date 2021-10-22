using AutoMapper;
using eShop.Catalog.API.Dtos;
using eShop.Catalog.API.Models;

namespace eShop.Catalog.API.Profiles
{
    public class CatalogsProfile : Profile
    {
        public CatalogsProfile()
        {
            CreateMap<Product,ProductReadDto>();
            CreateMap<ProductCreateDto,Product>();
        }
    }
}