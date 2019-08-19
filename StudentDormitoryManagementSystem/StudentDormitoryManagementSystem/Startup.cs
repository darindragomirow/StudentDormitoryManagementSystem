using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentDormitoryManagementSystem.Startup))]
namespace StudentDormitoryManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
