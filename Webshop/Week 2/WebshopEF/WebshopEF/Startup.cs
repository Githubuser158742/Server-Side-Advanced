using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebshopEF.Startup))]
namespace WebshopEF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
