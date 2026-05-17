using Catalog.API.Exceptions;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductRequest(Guid Id);
    public record DeleteProductResponse(bool IsSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/product/{id}", async (string? id, ISender sender) =>
            {
                try
                {
                    DeleteProductCommand command = new DeleteProductCommand(Guid.Parse(id));
                    DeleteProductResult result = await sender.Send(command);
                    return Results.Ok(result.Adapt<DeleteProductResponse>());
                }
                catch (ProductNotFoundException pnex)
                {
                    return Results.NotFound(pnex.Message);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }).Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product"); ;
        }
    }
}
