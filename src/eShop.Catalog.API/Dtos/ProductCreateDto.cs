using System.ComponentModel.DataAnnotations;

namespace eShop.Catalog.API.Dtos
{
    public class ProductCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Supplier { get; set; }
    }
}