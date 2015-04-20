using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GWSApp.Startup))]
namespace GWSApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
