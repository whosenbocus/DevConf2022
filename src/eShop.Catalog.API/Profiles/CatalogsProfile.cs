using AutoMapper;

public class CatalogsProfile : Profile
{
    public CatalogsProfile()
    {
        CreateMap<Product,ProductReadDto>();
        CreateMap<ProductCreateDto,Product>();
    }
}