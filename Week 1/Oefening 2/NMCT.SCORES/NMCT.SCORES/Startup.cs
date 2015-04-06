using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NMCT.SCORES.Startup))]
namespace NMCT.SCORES
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
