﻿using BuildingBlocks.CQRS;
using Ordering.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    public record GetOrdersByNameQuery(string Name):
        IQuery<GetOrderByNameResult>;
        public record GetOrderByNameResult(IEnumerable<OrderDto> Orders);
}
