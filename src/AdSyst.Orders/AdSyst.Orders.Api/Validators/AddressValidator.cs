using FluentValidation;
using AdSyst.Orders.DAL.MongoDb.Models;

namespace AdSyst.Orders.Api.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator(GeoPositionValidator positionValidator)
        {
            RuleFor(a => a.City).NotEmpty().MaximumLength(50);
            RuleFor(a => a.Street).NotEmpty().MaximumLength(50);
            RuleFor(a => a.House).NotEmpty().MaximumLength(10);
            RuleFor(a => a.Flat).NotEmpty().MaximumLength(10);

            When(
                a => a.GeoPosition != null,
                () => RuleFor(a => a.GeoPosition).SetValidator(positionValidator!)
            );
        }
    }
}
