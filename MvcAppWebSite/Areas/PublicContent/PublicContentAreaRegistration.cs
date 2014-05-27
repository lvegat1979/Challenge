using System.Web.Mvc;

namespace MvcAppWebSite.Areas.PublicContent
{
    public class PublicContentAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PublicContent";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "PublicContent_default",
                "PublicContent/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
