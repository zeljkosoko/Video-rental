using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute()); //all controllers dont allow anonymous member to see any view
            filters.Add(new RequireHttpsAttribute()); // Application requires only https channel
        }
    }
}
