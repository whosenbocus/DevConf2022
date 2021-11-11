public interface ICatalogService
{
    Task<IEnumerable<ProductRead>> GetProducts();
    Task<ProductRead> GetProductByID(string id);
    Task CreateProduct(ProductCreate productCreate);
}