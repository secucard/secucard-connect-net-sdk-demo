using System.Web;
using System.Web.Mvc;

namespace Secucard.Connect.DemoWebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
