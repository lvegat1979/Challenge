using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ChallengeRestFul
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
              name: "Result",
              routeTemplate: "api/championship/{controller}/{id}",
              defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
