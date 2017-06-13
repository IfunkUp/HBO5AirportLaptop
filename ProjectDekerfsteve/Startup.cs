using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectDekerfsteve.Startup))]
namespace ProjectDekerfsteve
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
