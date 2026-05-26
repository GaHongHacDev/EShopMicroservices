namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketResponse(string username);
    public record StoreBasketRequest(ShoppingCart Cart);
    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                StoreBasketCommand command = request.Adapt<StoreBasketCommand>();
                StoreBasketCommandResult result = await sender.Send(command);
                StoreBasketResponse response = result.Adapt<StoreBasketResponse>();
                return Results.Ok(response);
            })
            .WithName("StoreBasketEndpoint")
            .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Store Basket")
            .WithDescription("Store Basket");
        }
    }
}
