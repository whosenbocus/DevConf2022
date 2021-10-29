using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[Route("api/Basket/[controller]")]
[ApiController]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseRepo _repository;
    private readonly IMapper _mapper;
    private readonly IProductDataClient _productDataClient;
    private readonly IMessageBusClient _messageBusClient;

    public PurchaseController(
        IPurchaseRepo repository,
        IMapper mapper,
        IProductDataClient productDataClient,
        IMessageBusClient messageBusClient
        )
    {
        _repository = repository;
        _mapper = mapper;
        _productDataClient = productDataClient;
        _messageBusClient = messageBusClient;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PurchaseReadDto>> GetPurchasesForProduct(int productId)
    {
        if (!_repository.ProductExist(productId))
        {
            return NotFound();
        }

            var purchases = _repository.GetPurchaseForProduct(productId);

            return Ok(_mapper.Map<IEnumerable<PurchaseReadDto>>(purchases));
    }

    [HttpGet("{purchaseId}", Name ="GetPurchasesForProduct")]
    public ActionResult<PurchaseReadDto> GetPurchasesForProduct(int productId, int purchaseId)
    {
        var purchase = _repository.GetPurchase(productId, purchaseId);

        if(purchase == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<PurchaseReadDto>(purchase));
    }

    [HttpPost]
    public async Task<ActionResult<PurchaseReadDto>> CreatePurchaseForProduct(int productId, PurchaseCreateDto purchaseDto)
    {
        if (!_repository.ProductExist(productId))
        {
            return NotFound();
        }

        var product = _repository.GetProduct(productId);

        var purchase = _mapper.Map<Purchase>(purchaseDto);

        _repository.CreatePurchase(productId, purchase);
        _repository.SaveChanges();

        //await _productDataClient.DecreaseProduct(product.ExternalID,purchaseDto.Quantity);
        _messageBusClient.PublishPurchase(product.ExternalID,purchaseDto.Quantity);

        var PurchaseReadDto = _mapper.Map<PurchaseReadDto>(purchase);

        return CreatedAtRoute(nameof(GetPurchasesForProduct),
            new {productId = productId, purchaseId = PurchaseReadDto.Id}, PurchaseReadDto);
    }

}