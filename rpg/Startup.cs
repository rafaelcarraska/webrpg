using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(rpg.Startup))]
namespace rpg
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
