using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StaffTrainee.Startup))]
namespace StaffTrainee
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
