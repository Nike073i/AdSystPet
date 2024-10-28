using System.Runtime.CompilerServices;
using ErrorOr;

namespace AdSyst.Advertisments.Domain.Advertisments
{
    public static class AdvertismentErrors
    {
        public static Error IncorrectStateChangeError(AdvertismentStatus currentStatus) =>
            Error.Failure(
                code: CreateCode(),
                description: "Неккоректная смена статуса объявления",
                metadata: new() { { "CurrentStatus", currentStatus } }
            );

        public static readonly Error NotFound = Error.NotFound(
            code: CreateCode(),
            description: $"Объяление с указанным Id не найдено"
        );

        public static readonly Error Unauthorized = Error.Unauthorized(
            code: CreateCode(),
            description: $"Для взаимодействия с объявлением необходимо быть аутентифицированным"
        );

        public static readonly Error Forbidden = Error.Forbidden(
            code: CreateCode(),
            description: $"Недостаточно прав для взаимодействия с объявлением"
        );

        private static string CreateCode([CallerMemberName] string? propertyName = null) =>
            $"Advertisment.{propertyName}";
    }
}
