
namespace Basket.API.Basket.GetBasket
{
    public record GetBasketResponse(ShoppingCart ShoppingCart);
    public class GetBasketEnpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetBasketQuery(userName));
                return Results.Ok(result.Adapt<GetBasketResponse>());
            }).WithName("GetBasketEndpoint")
            .Produces<GetBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Basket")
            .WithDescription("Get Basket");
        }
    }
}
