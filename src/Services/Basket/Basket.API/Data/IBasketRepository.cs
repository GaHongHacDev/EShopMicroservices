namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        public Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default);
        public Task<string> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default);
        public Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default);
    }
}
