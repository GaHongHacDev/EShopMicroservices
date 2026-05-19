namespace Catalog.API.Products.GetProductByCategory
{
    internal record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    internal record GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var result = await session.Query<Product>()
                                    .Where(p => p.Category.Any(x => x.Contains(query.Category)))
                                    .ToListAsync(cancellationToken);
            return new GetProductByCategoryResult(result);
        }
    }
}
