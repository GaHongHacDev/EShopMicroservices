namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketCommandResult>;
    public record DeleteBasketCommandResult(bool IsDelete);
    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is not empty");
        }
    }
    public class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketCommandResult>
    {
        public async Task<DeleteBasketCommandResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            string userName = command.UserName;
            // delete from DB
            // delete from cache
            return new DeleteBasketCommandResult(true);
        }
    }
}
