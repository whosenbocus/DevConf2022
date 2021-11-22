using System.Text.Json;
using AutoMapper;
using Dapr;
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

    [Topic("eshopqueue","Product")]
    [HttpPost("Product")]
    public ActionResult CreateProduct(ProductCreateDto productDto)
    {
        Console.WriteLine($"--> Receiving Product {JsonSerializer.Serialize(productDto)}");
        var product = _mapper.Map<Product>(productDto);
        _repository.CreateProduct(product);
        _repository.SaveChanges();
        return Ok();
    }

}