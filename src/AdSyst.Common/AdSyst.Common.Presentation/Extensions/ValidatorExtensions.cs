using System.Text.RegularExpressions;
using FluentValidation;

namespace AdSyst.Common.Presentation.Extensions
{
    public static partial class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> MustBeGuid<T>(
            this IRuleBuilder<T, string> ruleBuilder
        ) => ruleBuilder.NotEmpty().Must(IsGuid).WithMessage("Значение должно быть типа UUID");

        private static bool IsGuid(string value) => Guid.TryParse(value, out var _);

        public static IRuleBuilderOptions<T, string> MustBeMongoId<T>(
            this IRuleBuilder<T, string> ruleBuilder
        ) =>
            ruleBuilder.NotEmpty().Must(IsMongoId).WithMessage("Значение должно быть типа MongoId");

        private static bool IsMongoId(string value)
        {
            var regex = MongoIdRegex();
            return value != null && regex.IsMatch(value);
        }

        [GeneratedRegex("^[0-9a-f]{24}$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
        private static partial Regex MongoIdRegex();
    }
}
