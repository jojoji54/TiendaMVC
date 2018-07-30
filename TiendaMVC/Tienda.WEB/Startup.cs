using Microsoft.Owin;
using Owin;
using Tienda.WEB;

[assembly: OwinStartupAttribute(typeof(Tienda.WEB.Startup))]
namespace Tienda.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
