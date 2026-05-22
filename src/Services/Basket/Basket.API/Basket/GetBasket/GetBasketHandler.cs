namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IRequest<GetBasketResult>;
    public record GetBasketResult(ShoppingCart ShoppingCart);
    public class GetBasketHandler : IRequestHandler<GetBasketQuery, GetBasketResult>
    {
        public Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new GetBasketResult(new ShoppingCart("aws")));
        }
    }
}
