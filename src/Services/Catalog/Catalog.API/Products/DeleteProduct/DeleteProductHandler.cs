
using Catalog.API.Exceptions;

namespace Catalog.API.Products.DeleteProduct
{
    internal record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    internal record DeleteProductResult(bool IsSuccess);
    internal class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID cannot be empty.");
        }
    }

    internal class DeleteProductCommandHandler(IDocumentSession session)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException(request.Id);
            }
            session.Delete(product);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }
    }
}
