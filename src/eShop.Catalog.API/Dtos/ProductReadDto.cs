namespace eShop.Catalog.API.Dtos
{
    public class ProductReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Supplier { get; set; }
    }
}