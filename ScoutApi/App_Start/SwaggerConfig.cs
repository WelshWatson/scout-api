using System.Web.Http;
using WebActivatorEx;
using ScoutApi;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace ScoutApi
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c => c.SingleApiVersion("v1", "ScoutApi"))
                .EnableSwaggerUi();
        }
    }
}
