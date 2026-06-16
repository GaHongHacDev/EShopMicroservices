namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketCommandResult>;
    public record StoreBasketCommandResult(string username);
    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart is not null");
            RuleFor(x => x.Cart.Username).NotEmpty().WithMessage("Username is not empty");
        }
    }
    public class StoreBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketCommandResult>
    {
        public async Task<StoreBasketCommandResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            ShoppingCart cart = request.Cart;
            // Store to DB
            // update cache
            await basketRepository.StoreBasket(cart, cancellationToken);
            return new StoreBasketCommandResult(cart.Username);
        }
    }
}
