using System.Text.Json;
using AutoMapper;
using Dapr;
using Microsoft.AspNetCore.Mvc;

[Route("api/Catalog/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepo _repository;
    private readonly IMapper _mapper;
    private readonly IPurchaseDataClient _purchaseDataClient;
    private readonly IMessageBusClient _messageBusClient;

    public ProductController(
        IProductRepo repository,
        IMapper mapper,
        IPurchaseDataClient purchaseDataClient,
        IMessageBusClient messageBusClient
        )
    {
        _repository = repository;
        _mapper = mapper;
        _purchaseDataClient = purchaseDataClient;
        _messageBusClient = messageBusClient;
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

        //await _purchaseDataClient.CreateProduct(productReadDto.Id,productReadDto.Name);

        await _messageBusClient.PublishNewProduct(productReadDto.Id,productReadDto.Name);

        return CreatedAtRoute(nameof(GetProductById), new { Id = productReadDto.Id}, productCreateDto);
    }

    [Topic("eshopqueue","Purchase")]
    [HttpPost("Purchase")]
    public ActionResult<ProductReadDto> DecreaseProductById(ProductDecrease productDecrease)
    {
        Console.WriteLine($"--> Receiving Purchase {JsonSerializer.Serialize(productDecrease)}");
        _repository.DecreaseProductQuantity(productDecrease.productId, productDecrease.amount);
        _repository.SaveChanges();
        var productItem = _repository.GetProductById(productDecrease.productId);
        if (productItem != null)
        {
            return Ok(_mapper.Map<ProductReadDto>(productItem));
        }

        return NotFound();
    }

}