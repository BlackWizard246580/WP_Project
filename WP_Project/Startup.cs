using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WP_Project.Startup))]
namespace WP_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
