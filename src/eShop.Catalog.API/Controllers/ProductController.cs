using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepo _repository;
    private readonly IMapper _mapper;
    private readonly IPurchaseDataClient _purchaseDataClient;

    public ProductController(
        IProductRepo repository,
        IMapper mapper,
        IPurchaseDataClient purchaseDataClient
        )
    {
        _repository = repository;
        _mapper = mapper;
        _purchaseDataClient = purchaseDataClient;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProductReadDto>> GetProducts()
    {
        var productItem = _repository.GetAllProducts();

        return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(productItem));
    }

    [HttpGet("{id}", Name = "GetProductById")]
    public ActionResult<ProductReadDto> GetProductById(int id)
    {
        var productItem = _repository.GetProductById(id);
        if (productItem != null)
        {
            return Ok(_mapper.Map<ProductReadDto>(productItem));
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<ProductReadDto>> CreateProduct(ProductCreateDto productCreateDto)
    {
        var productModel = _mapper.Map<Product>(productCreateDto);
        _repository.CreateProduct(productModel);
        _repository.SaveChanges();

        var productReadDto = _mapper.Map<ProductReadDto>(productModel);

        await _purchaseDataClient.CreateProduct(productReadDto.Id,productReadDto.Name);

        return CreatedAtRoute(nameof(GetProductById), new { Id = productReadDto.Id}, productCreateDto);
    }

    [HttpPut("{id}", Name = "DecreaseProductById")]
    public ActionResult<ProductReadDto> DecreaseProductById(int id, int Amount)
    {
        _repository.DecreaseProductQuantity(id, Amount);
        _repository.SaveChanges();
        var productItem = _repository.GetProductById(id);
        if (productItem != null)
        {
            return Ok(_mapper.Map<ProductReadDto>(productItem));
        }

        return NotFound();
    }

}