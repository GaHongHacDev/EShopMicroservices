using BuildingBlocks.NewFolder;

namespace Basket.API.Exceptions
{
    public class BasketNotFound : NotFoundException
    {
        public BasketNotFound(string key) : base("Basket", key)
        {
        }
    }
}
