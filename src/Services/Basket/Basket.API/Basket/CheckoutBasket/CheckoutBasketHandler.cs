﻿using Basket.API.Dtos;
using BuildingBlocks.Messaging.Events;
using FluentValidation;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto):ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);
    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(b => b.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto can not be null.");
            RuleFor(b => b.BasketCheckoutDto).NotEmpty().WithMessage("BasketCheckoutDto can not be empty.");

        }
    }
    public class CheckoutBasketCommandHandler(IBasketRepository repository ,IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async  Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            //get existing basket with total price 

            //set tota price on basketcheckout event message 
            //send basket checkout event to rabbitmq using masstransit
            //delete the basket 
            var basket = await repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken: cancellationToken);
            if (basket == null)
                return new CheckoutBasketResult(false);
            var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;
            await publishEndpoint.Publish(eventMessage, cancellationToken);
            await repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);
            return new CheckoutBasketResult(true);


            




        }
    }
}
