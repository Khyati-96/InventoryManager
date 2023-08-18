using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InventoryMasterNew.Startup))]
namespace InventoryMasterNew
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
