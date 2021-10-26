using AutoMapper;

public class PurchaseProfile : Profile
{
    public PurchaseProfile()
    {
        CreateMap<Product,ProductReadDto>();
        CreateMap<ProductCreateDto,Product>();
        CreateMap<Purchase,PurchaseReadDto>();
        CreateMap<PurchaseCreateDto,Purchase>();
    }
}