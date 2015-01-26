using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Infrastructure
{
    public static class MenuItemExtension
    {
        public static MvcHtmlString MenuItem(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            string currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            //string currentController = htmlHelper.ViewContext.ParentActionViewContext.RouteData.GetRequiredString("controller");
            string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            var tb = new TagBuilder("li");
            tb.AddCssClass("nav");

            if (actionName == currentAction && controllerName == currentController)
            {
                tb.AddCssClass("active");
            }
            tb.InnerHtml = htmlHelper.ActionLink(linkText, actionName, controllerName).ToHtmlString();

            return new MvcHtmlString(tb.ToString());
        }
    }
}
