using System.Web;
using System.Web.Mvc;

namespace IlseLeijten
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new LanguageFilterAttribute(new CultureManager()));
            //filters.Add(new RedirectFilterAttribute(new RedirectionManager()));
        }
    }
}
