

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IRequest<GetBasketResult>;
    public record GetBasketResult(ShoppingCart ShoppingCart);
    public class GetBasketHandler(IBasketRepository basketRepository) : IRequestHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasket(request.UserName, cancellationToken);
            return new GetBasketResult(basket);
            //return Task.FromResult(new GetBasketResult(new ShoppingCart("aws")));
        }
    }
}
