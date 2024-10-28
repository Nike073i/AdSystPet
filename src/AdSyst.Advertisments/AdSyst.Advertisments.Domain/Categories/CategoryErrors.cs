using System.Runtime.CompilerServices;
using ErrorOr;

namespace AdSyst.Advertisments.Domain.Categories
{
    public static class CategoryErrors
    {
        public static readonly Error NotFound = Error.NotFound(
            code: CreateCode(),
            description: $"Категория с указанным Id не найдена"
        );

        public static readonly Error CannotBeRemovedBecauseThereAreChildCategories = Error.Failure(
            code: CreateCode(),
            description: $"Категория имеет дочерние категории и не может быть удалена"
        );

        public static readonly Error CannotBeRemovedBecauseItIsUsedInAdvertisment = Error.Failure(
            code: CreateCode(),
            description: $"Категория используется в объявлениях"
        );

        private static string CreateCode([CallerMemberName] string? propertyName = null) =>
            $"Category.{propertyName}";
    }
}
