using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BackgroundSpike.Startup))]
namespace BackgroundSpike
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
