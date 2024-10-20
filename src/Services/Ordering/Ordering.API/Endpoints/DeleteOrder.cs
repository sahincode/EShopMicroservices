
using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints
{
    //- Accept the order ID as a parameter .
    //- Constract a DeleteOrderCommand.
    //- Sends the command using MediatR.
    //- Returns a success or not found response.
    public record DeleteOrderRequest(Guid Id);

    public record DeleteOrderResponse(bool ISSuccess);
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapDelete("/orders/{id}", async (Guid Id, ISender sender) =>
           {
               var result = await sender.Send(new DeleteOrderCommand(Id));
               var response = result.Adapt<DeleteOrderResponse>();
               return Results.Ok(response);

           }).WithName("DeleteOrder")
            .Produces<CreateOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Create Order")
            .WithDescription("Create Order");
        }
    }
}
