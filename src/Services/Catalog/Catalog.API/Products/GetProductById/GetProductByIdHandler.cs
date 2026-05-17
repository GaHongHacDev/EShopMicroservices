using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductById
{
    internal record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    internal record GetProductByIdResult(Product Product);

    internal class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if (result == null)
            {
                throw new ProductNotFoundException($"Product with id {request.Id} not found");
            }
            return new GetProductByIdResult(result);
        }
    }
}
