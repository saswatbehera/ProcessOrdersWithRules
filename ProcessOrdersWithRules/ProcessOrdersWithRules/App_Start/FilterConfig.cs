using System.Web;
using System.Web.Mvc;

namespace ProcessOrdersWithRules
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
