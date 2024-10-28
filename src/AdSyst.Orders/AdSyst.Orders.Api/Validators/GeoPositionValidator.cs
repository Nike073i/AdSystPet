using FluentValidation;
using AdSyst.Orders.DAL.MongoDb.Models;

namespace AdSyst.Orders.Api.Validators
{
    public class GeoPositionValidator : AbstractValidator<GeoPosition>
    {
        public GeoPositionValidator()
        {
            RuleFor(a => a.Latitude).InclusiveBetween(-90, 90);
            RuleFor(a => a.Longitude).InclusiveBetween(-180, 180);
        }
    }
}
