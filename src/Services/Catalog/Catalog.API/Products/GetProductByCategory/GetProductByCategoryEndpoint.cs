namespace Catalog.API.Products.GetProductByCategory
{
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public record GetProductByCategoryResponse(IEnumerable<Product> Products);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                GetProductByCategoryQuery query = new GetProductByCategoryQuery(category);
                GetProductByCategoryResult result = await sender.Send(query);
                GetProductByCategoryResponse response = result.Adapt<GetProductByCategoryResponse>();
                return Results.Ok(response);
            }).WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products By Category")
            .WithDescription("Get Product By Category");
        }
    }
}
