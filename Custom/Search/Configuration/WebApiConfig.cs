using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SitefinityWebApp.Custom.Search.Models;

namespace SitefinityWebApp.Custom.Search.Configuration
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //DECLARE REGEX PATTERNS
            string alphanumeric = @"^[a-zA-Z]+[_ a-zA-Z0-9]*$";
            string numeric = @"^\d+$";

            //For Generic Api
            config.Routes.MapHttpRoute(
                name: "RGPApiControllerActionId",
                routeTemplate: Constants.VALUE_WEBAPI_ROOT_PATH + "/{controller}/{action}/{id}",
                defaults: null,
                constraints: new { action = alphanumeric, id = new GuidConstraint() } // action must start with character
            );

            config.Routes.MapHttpRoute(
                name: "RGPApiControllerActionName",
                routeTemplate: Constants.VALUE_WEBAPI_ROOT_PATH + "/{controller}/{action}/{name}",
                defaults: null,
                constraints: new { action = alphanumeric, name = alphanumeric } // action and name must start with character
            );

            config.Routes.MapHttpRoute(
                name: "RGPApiControllerId",
                routeTemplate: Constants.VALUE_WEBAPI_ROOT_PATH + "/{controller}/{id}",
                defaults: new { action = "Get" },
                constraints: new { id = new GuidConstraint() } // id must be a guid
            );

            config.Routes.MapHttpRoute(
                name: "RGPApiControllerAction",
                routeTemplate: Constants.VALUE_WEBAPI_ROOT_PATH + "/{controller}/{action}",
                defaults: null,
                constraints: new { action = alphanumeric } // action must start with character
            );

            //TODO: FOR HTTP VERB BASED ROUTING
            config.Routes.MapHttpRoute(
                name: "RGPApiControllerGet",
                routeTemplate: Constants.VALUE_WEBAPI_ROOT_PATH + "/{controller}",
                defaults: new { action = "Get" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: "RGPApiControllerPost",
                routeTemplate: Constants.VALUE_WEBAPI_ROOT_PATH + "/{controller}",
                defaults: null,
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) }
            );

            config.Routes.MapHttpRoute(
                name: "RGPApiControllerPut",
                routeTemplate: Constants.VALUE_WEBAPI_ROOT_PATH + "/{controller}",
                defaults: null,
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) }
            );

            config.Routes.MapHttpRoute(
                name: "RGPApiControllerDelete",
                routeTemplate: Constants.VALUE_WEBAPI_ROOT_PATH + "/{controller}",
                defaults: new { action = "Delete" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) }
            );

            config.Formatters.Clear();
            var jsonFormatter = new JsonMediaTypeFormatter();
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.Add(jsonFormatter);
            
        }
    }
}