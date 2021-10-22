using AutoMapper;
using eShop.Catalog.API.Data;
using eShop.Catalog.API.Dtos;
using eShop.Catalog.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _repository;
        private readonly IMapper _mapper;

        public ProductController(
            IProductRepo repository,
            IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
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
        public ActionResult<ProductReadDto> CreateProduct(ProductCreateDto productCreateDto)
        {
            var productModel = _mapper.Map<Product>(productCreateDto);
            _repository.CreateProduct(productModel);
            _repository.SaveChanges();

            var productReadDto = _mapper.Map<ProductReadDto>(productModel);

            return CreatedAtRoute(nameof(GetProductById), new { Id = productReadDto.Id}, productCreateDto);
        }

        [HttpPut("{id}", Name = "DecreaseProductById")]
        public ActionResult<ProductReadDto> DecreaseProductById(int id)
        {
            _repository.DecreaseProductQuantity(id);
            _repository.SaveChanges();
            var productItem = _repository.GetProductById(id);
            if (productItem != null)
            {
                return Ok(_mapper.Map<ProductReadDto>(productItem));
            }

            return NotFound();
        }

    }
}