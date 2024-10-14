using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    public class GetOrdersByNameHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrderByNameResult>
    {
        public async  Task<GetOrderByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            //get order by name using dbcontext 
            //return result
            var orders = await dbContext.Orders.
                 Include(o => o.OrderItems)
                 .AsTracking()
                 .Where(o => o.OrderName.Value.Contains(query.Name))
                 .OrderBy(o => o.OrderName)
                 .ToListAsync(cancellationToken);
        }
    }
}
