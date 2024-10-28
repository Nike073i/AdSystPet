using FluentValidation;
using AdSyst.Orders.Api.Models;

namespace AdSyst.Orders.Api.Validators
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator(AddressValidator addressValidator)
        {
            RuleFor(d => d.AdvertismentId).NotEmpty();
            RuleFor(d => d.AddressTo).NotNull().SetValidator(addressValidator);
        }
    }
}
