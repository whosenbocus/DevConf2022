public interface IProductDataClient
{
    Task DecreaseProduct(int productId, int amount);
}