using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OAuthServer.Api.App_Start;
using System.Web.Http;

namespace OAuthServer.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonformatter = formatters.JsonFormatter;
            var settings = jsonformatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
