﻿using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.GetProduct
{
    public  record GetProductRequest(int? PageNumber=1 ,int PageSize =2);
    
   public record GetProductsResponse(IEnumerable<Product> Products);
    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductRequest request ,ISender sender) =>
            {
                var query = request.Adapt<GetProductQuery>();

                var result = await sender.Send(query);
                var response = result.Adapt<GetProductsResponse>();
                return Results.Ok(response);
            }).WithName("GetProducts").
            Produces<GetProductsResponse>(StatusCodes.Status200OK).
            ProducesProblem(StatusCodes.Status400BadRequest).
            WithSummary("Get Products").
            WithDescription("Get Products");

        }
    }
}
