using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToonSaloon.Web.Startup))]
namespace ToonSaloon.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
