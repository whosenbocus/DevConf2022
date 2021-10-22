using eShop.Catalog.API.Models;

namespace eShop.Catalog.API.Data
{
    public interface IProductRepo
    {
        bool SaveChanges();

        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void CreateProduct(Product product);
        void DecreaseProductQuantity(int id);
    }
}