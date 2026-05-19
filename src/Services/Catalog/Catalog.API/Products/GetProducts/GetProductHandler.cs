namespace Catalog.API.Products.GetProducts
{
    internal record GetProductQuery() : IQuery<GetProductResult>;
    internal record GetProductResult(IEnumerable<Product> Products);
    internal class GetProductQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var result = await session.Query<Product>().ToListAsync();
            return new GetProductResult(result);
        }
    }
}
