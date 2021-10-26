using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[Route("api/Basket/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
        private readonly IPurchaseRepo _repository;
    private readonly IMapper _mapper;
    private readonly IProductDataClient _productDataClient;
    public ProductController(
    IPurchaseRepo repository,
    IMapper mapper,
    IProductDataClient productDataClient
    )
    {
        _repository = repository;
        _mapper = mapper;
        _productDataClient = productDataClient;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProductReadDto>> GetProducts()
    {

        var productItems = _repository.GetAllProducts();

        return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(productItems));
    }

    [HttpPost]
    public ActionResult CreateProduct(ProductCreateDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        _repository.CreateProduct(product);
        _repository.SaveChanges();
        return Ok();
    }

}