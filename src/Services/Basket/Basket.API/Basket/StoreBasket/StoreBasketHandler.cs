
using Discount.Grpc;
using FluentValidation;
using MediatR;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);
    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(s => s.Cart).NotNull().WithMessage("Can not be null");

            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("USerName is required.");
        }
    }
    public class StoreBasketCommandHandler(IBasketRepository repository,DiscountProtoService.DiscountProtoServiceClient discount) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async  Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await DeducDiscount(command.Cart, cancellationToken);
            await repository.StoreBasket(command.Cart, cancellationToken);
            return new StoreBasketResult(command.Cart.UserName);
        }
        public async Task DeducDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in command.Cart.Items)
            {
                var coupon = await discount.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }
        }
    }
   
}
