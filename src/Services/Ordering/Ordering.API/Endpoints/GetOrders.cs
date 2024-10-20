﻿using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints
{
    //- Accepts pagination paraemters.
    //- Constracts a GetOrdersQuery with these parameters.
    //- Retrieves the data and returns it in a paginated format.
    //public record GetOrdersRequest(PaginationRequest PaginationRequest);
    public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);


    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapGet("/orders", async([AsParameters] PaginationRequest request, ISender sender) =>
           {
               var result = await sender.Send(new GetOrdersQuery(request));
               var response = result.Adapt<GetOrdersQuery>();
               return Results.Ok(response);
           }).WithName("GetOrders")
            .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Orders")
            .WithDescription("Get Orders");
        }
    }
}
