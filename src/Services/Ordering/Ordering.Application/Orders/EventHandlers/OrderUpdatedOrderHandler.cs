using Microsoft.Extensions.Logging;
using Ordering.Application.Orders.Commands.UpdateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandlers
{
    internal class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger) :INotificationHandler<OrderUpdateEvent>
    {
        public Task Handle(OrderUpdateEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handler :{DomainEvent}", notification.GetType());
            return Task.CompletedTask;
        }
    }
}

