using System;
using System.Web;
using System.Web.Routing;

namespace SitefinityWebApp.Custom.Search.Models
{
    /// <summary>
    /// A constratint used for routes
    /// Use when mapping routes: constraints: new { id = new GuidConstraint() }
    /// </summary>
    public class GuidConstraint : IRouteConstraint
    {
        /// <summary>
        /// Determines whether the URL parameter contains a valid value for this constraint.
        /// </summary>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
        /// <param name="route">The object that this constraint belongs to.</param>
        /// <param name="parameterName">The name of the parameter that is being checked.</param>
        /// <param name="values">An object that contains the parameters for the URL.</param>
        /// <param name="routeDirection">An object that indicates whether the constraint check is being performed when an incoming request is being handled or when a URL is being generated.</param>
        /// <returns>
        /// true if the URL parameter contains a valid value; otherwise, false.
        /// </returns>
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            object value;
            if (!values.TryGetValue(parameterName, out value)) return false;
            if (value is Guid) return true;

            var stringValue = Convert.ToString(value);
            if (string.IsNullOrWhiteSpace(stringValue)) return false;

            Guid guidValue;
            if (!Guid.TryParse(stringValue, out guidValue)) return false;
            if (guidValue == Guid.Empty) return false;

            return true;
        }
    }
}