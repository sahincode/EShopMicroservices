
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints
{
    //- Accept a UpdateOrderComand.
    //- Maps the request to an UpdateOrderCommand.
    //- Sends the command for proccessing.
    //- Returns a success or error response based on the outcome.

    public record UpdateOrderRequest(OrderDto Order);
    public record UpdateOrderResponse(bool  IsSuccess);

    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateOrderCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateOrderResponse>();
                return Results.Ok(response);


            })
        }
    }
}
