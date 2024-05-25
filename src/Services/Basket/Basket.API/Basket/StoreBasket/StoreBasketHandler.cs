using FluentValidation;
using MediatR;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string userName);
    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(s => s.Cart).NotNull().WithMessage("Can not be null");

            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("USerName is required.");
        }
    }
    public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async  Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.Cart;
            return new StoreBasketResult("sen");
        }
    }
}
