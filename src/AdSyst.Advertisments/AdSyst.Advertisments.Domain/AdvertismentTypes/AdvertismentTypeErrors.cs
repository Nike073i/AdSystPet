using System.Runtime.CompilerServices;
using ErrorOr;

namespace AdSyst.Advertisments.Domain.AdvertismentTypes
{
    public static class AdvertismentTypeErrors
    {
        public static readonly Error NotFound = Error.NotFound(
            code: CreateCode(),
            description: $"Тип объявления с указанным Id не найден"
        );

        public static readonly Error CannotBeRemovedBecauseItIsUsedInAdvertisment = Error.Failure(
            code: CreateCode(),
            description: $"Тип объявления используется в объявлениях"
        );

        private static string CreateCode([CallerMemberName] string? propertyName = null) =>
            $"AdvertismentType.{propertyName}";
    }
}
