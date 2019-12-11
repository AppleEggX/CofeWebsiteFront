using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CofeWebApplication.Startup))]
namespace CofeWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
