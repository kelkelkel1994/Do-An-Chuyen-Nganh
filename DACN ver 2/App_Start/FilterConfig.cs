using DACN_ver_2.Filters;
using System.Web;
using System.Web.Mvc; 
namespace DACN_ver_2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RecaptchaFilter());
        }
    }
}
