

using BuildingBlocks.Messaging.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler(ISender sender ,ILogger<BasketCheckoutEventHandler> logger) : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            //TODO:Create new order and start order fullfillment process

            logger.LogInformation("Integration Event handler :{IntegrationEvent}", context.Message.GetType().Name);
            var command = MapToCreateOrderCommand(context.Message);
            await sender.Send(command);

        }
        private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
        {
            var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
            var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);
            var orderId = Guid.NewGuid();
            var orderDto = new OrderDto(Id: orderId,
                CustomerId: message.CustomerId,
                 OrderName: message.UserName,
                 ShippingAddress: addressDto,
                 BillingAddress: addressDto,
                 Payment: paymentDto,
                 Status: Ordering.Domain.Enums.OrderStatus.Pending,
                 OrderItems: [
                     new OrderItemDto(orderId, new Guid("be1bece4-5708-4daf-92e5-5f9fabf51a4a"),1 ,500
                     ),
                       new OrderItemDto(orderId, new Guid("7ea9b3fb-1f01-48b4-9e97-8346fe96caea"),1 ,500
                     )
                     ]


                 );
            return new CreateOrderCommand(orderDto);

        }
    }
}
