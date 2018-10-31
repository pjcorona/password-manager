using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Password_Manager_WebApp.Startup))]
namespace Password_Manager_WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
