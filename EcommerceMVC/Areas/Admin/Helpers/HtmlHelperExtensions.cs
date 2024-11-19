using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceMVC.Areas.Admin.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent NavigationLink(
            this IHtmlHelper html,
            string linkText,
            string areaName,
            string actionName,
            string controllerName)
        {
            string contextArea = (string)html.ViewContext.RouteData.Values["area"];
            string contextAction = (string)html.ViewContext.RouteData.Values["action"];
            string contextController = (string)html.ViewContext.RouteData.Values["controller"];

            bool isCurrent =
                string.Equals(contextArea, areaName, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(contextAction, actionName, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(contextController, controllerName, StringComparison.CurrentCultureIgnoreCase);

            return html.ActionLink(
                linkText,
                actionName,
                controllerName,
                routeValues: new { area = areaName },
                htmlAttributes: isCurrent ? new { @class = "nav-link active" } : new { @class = "nav-link" });
        }
    }
}