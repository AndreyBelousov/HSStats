using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HSStats.Startup))]
namespace HSStats
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
