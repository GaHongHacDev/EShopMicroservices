
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketResponse(bool IsDelete);
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
            {
                DeleteBasketCommand command = new DeleteBasketCommand(userName);
                DeleteBasketCommandResult result = await sender.Send(command);
                DeleteBasketResponse response = result.Adapt<DeleteBasketResponse>();
                return Results.Ok(response);
            })
            .WithName("DeleteBasketEndpoint")
            .Produces<DeleteBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Basket")
            .WithDescription("Delete Basket");
        }
    }
}
