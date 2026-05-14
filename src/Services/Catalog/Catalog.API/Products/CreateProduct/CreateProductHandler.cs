namespace Catalog.API.Products.CreateProduct
{
    internal record CreateProductCommand(string Name, List<string> Category, string Description,
        string ImageFile, decimal Price) : ICommand<CreateProductResult>;

    internal record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler(IDocumentSession sesion) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                Name = command.Name,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
                Category = command.Category
            };

            sesion.Store(product);
            await sesion.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);
        }
    }
}
