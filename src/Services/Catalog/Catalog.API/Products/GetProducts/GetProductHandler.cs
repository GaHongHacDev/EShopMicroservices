using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{
    internal record GetProductQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductResult>;
    internal record GetProductResult(IEnumerable<Product> Products);
    internal class GetProductQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var result = await session.Query<Product>().ToPagedListAsync(request.PageNumber ?? 1, request.PageSize ?? 10, cancellationToken);
            return new GetProductResult(result);
        }
    }
}
