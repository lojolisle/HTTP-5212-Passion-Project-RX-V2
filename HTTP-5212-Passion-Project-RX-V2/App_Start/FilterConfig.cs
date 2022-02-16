using System.Web;
using System.Web.Mvc;

namespace HTTP_5212_Passion_Project_RX_V2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
