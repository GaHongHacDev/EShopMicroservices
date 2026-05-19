namespace Catalog.API.Products.GetProducts
{
    public record GetProductRequest(int? PageNumber = 1, int? PageSize = 10);
    public record GetProductResponse(IEnumerable<Product> Products);
    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductRequest request, ISender sender) =>
            {
                GetProductQuery query = request.Adapt<GetProductQuery>();
                GetProductResult result = await sender.Send(query);
                GetProductResponse response = result.Adapt<GetProductResponse>();
                return Results.Ok(response);
            }).WithName("GetProduct")
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Product");
        }
    }
}
