using System.Runtime.CompilerServices;
using ErrorOr;

namespace AdSyst.AuthService.Domain
{
    public static class UserErrors
    {
        public static readonly Error UserAlreadyExists = Error.Conflict(
            code: CreateCode(),
            description: "Пользователь уже существует в системе"
        );

        public static readonly Error NotFound = Error.NotFound(
            code: CreateCode(),
            description: "Пользователь с указанным Id не найден"
        );

        public static readonly Error UserWithoutRole = Error.Failure(
            code: CreateCode(),
            description: "Пользователь должен иметь хотя бы одну роль"
        );

        public static Error UserCannotBeCreated(Dictionary<string, object> errors) =>
            Error.Failure(
                code: CreateCode(),
                description: "Пользователь не может быть создан в системе",
                metadata: errors
            );

        public static Error EmailCannotBeConfirmed(Dictionary<string, object> errors) =>
            Error.Failure(
                code: CreateCode(),
                description: "Почта не может быть подтверждена",
                metadata: errors
            );

        public static readonly Error Unauthorized = Error.Unauthorized(
            code: CreateCode(),
            description: $"Для взаимодействия с пользователем необходимо быть аутентифицированным"
        );

        private static string CreateCode([CallerMemberName] string? propertyName = null) =>
            $"Users.{propertyName}";
    }
}
