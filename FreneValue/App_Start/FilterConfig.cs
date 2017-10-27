using System.Web;
using System.Web.Mvc;
using FreneValue.Infrastructure;

namespace FreneValue
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
           filters.Add(new ExceptionLoggingFilter());
        }
    }
}
