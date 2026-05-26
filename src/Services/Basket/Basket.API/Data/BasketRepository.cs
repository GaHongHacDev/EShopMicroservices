
namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {

        public async Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken)
        {
            ShoppingCart? shoppingCart = await session.LoadAsync<ShoppingCart>(username, cancellationToken);
            return shoppingCart is null ? throw new BasketNotFound(username) : shoppingCart;
        }

        public async Task<string> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken)
        {
            session.Store<ShoppingCart>(cart);
            await session.SaveChangesAsync(cancellationToken);
            return cart.Username;
        }

        public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken)
        {
            session.Delete<ShoppingCart>(username);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
