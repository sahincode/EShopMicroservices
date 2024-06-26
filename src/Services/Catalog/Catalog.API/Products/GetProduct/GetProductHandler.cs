﻿using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;
using Marten.Pagination;

namespace Catalog.API.Products.GetProduct
{
    public record GetProductQuery(int? PageNumber = 1, int? PageSize = 2) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(IDocumentSession session , ILogger<GetProductsQueryHandler> logger) : IQueryHandler<GetProductQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler.Handle called with {@Query}", query);
            var products =await session.Query<Product>().ToPagedListAsync( query.PageNumber ?? 1 , query.PageSize ?? 10,cancellationToken);
            return new GetProductsResult(products);
        }
    }
}
