using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DACN_ver_2.Startup))]
namespace DACN_ver_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
