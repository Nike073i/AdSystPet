using System.Runtime.CompilerServices;
using ErrorOr;

namespace AdSyst.Moderation.DAL.MongoDb.Errors
{
    public static class AdvertismentErrors
    {
        public static readonly Error AlreadyExists = Error.Conflict(
            CreateCode(),
            "Данные по объявлению уже существуют"
        );

        public static readonly Error HasNotes = Error.Failure(
            CreateCode(),
            "Объявление имеет активные замечания"
        );

        public static readonly Error NoteAlreadyDisabled = Error.Failure(
            CreateCode(),
            "Замечание уже неактивно"
        );

        public static readonly Error NotFound = Error.NotFound(
            CreateCode(),
            $"Объяление не найдено"
        );

        public static readonly Error NoteNotFound = Error.NotFound(
            CreateCode(),
            $"Замечание не найдено"
        );

        public static readonly Error Unauthorized = Error.Unauthorized(
            CreateCode(),
            $"Для взаимодействия с объявлением необходимо быть аутентифицированным"
        );

        public static readonly Error Forbidden = Error.Forbidden(
            CreateCode(),
            $"Недостаточно прав для взаимодействия с объявлением"
        );

        private static string CreateCode([CallerMemberName] string? propertyName = null) =>
            $"Advertisments.{propertyName}";
    }
}
