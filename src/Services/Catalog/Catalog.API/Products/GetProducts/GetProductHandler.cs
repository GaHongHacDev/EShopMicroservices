namespace Catalog.API.Products.GetProducts
{
    internal record GetProductQuery() : IQuery<GetProductResult>;
    internal record GetProductResult(IEnumerable<Product> Products);
    internal class GetProductQueryHandler(IDocumentSession session, ILogger<GetProductQueryHandler> logger)
        : IQueryHandler<GetProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductQuery call with {@Request}", request);
            var result = await session.Query<Product>().ToListAsync();
            return new GetProductResult(result);
        }
    }
}
