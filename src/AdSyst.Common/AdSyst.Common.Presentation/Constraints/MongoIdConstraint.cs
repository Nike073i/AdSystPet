using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AdSyst.Common.Presentation.Constraints
{
    public partial class MongoIdConstraint : IRouteConstraint
    {
        public const string MongoIdConstraintName = "mongoId";

        public bool Match(
            HttpContext? httpContext,
            IRouter? route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection
        )
        {
            var regex = MongoIdRegex();

            if (!values.TryGetValue(routeKey, out object? value))
                return false;
            string? parameterValueString = Convert.ToString(value, CultureInfo.InvariantCulture);
            return parameterValueString != null && regex.IsMatch(parameterValueString);
        }

        [GeneratedRegex("^[0-9a-f]{24}$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
        private static partial Regex MongoIdRegex();
    }
}
