using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GymPassBokning.Startup))]
namespace GymPassBokning
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
