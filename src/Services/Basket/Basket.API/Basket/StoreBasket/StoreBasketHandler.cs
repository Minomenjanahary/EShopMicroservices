namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResponse>;
    public record StoreBasketResponse(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(s => s.Cart).NotNull().WithMessage("Cart can not be null");
            RuleFor(s => s.Cart.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }

    internal class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResponse>
    {
        public async Task<StoreBasketResponse> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await repository.StoreBasket(command.Cart, cancellationToken);
            return new StoreBasketResponse(basket.UserName);
        }
    }
}