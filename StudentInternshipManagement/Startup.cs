using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentInternshipManagement.Startup))]
namespace StudentInternshipManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
