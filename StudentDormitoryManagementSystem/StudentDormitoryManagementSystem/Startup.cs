using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentDormitoryManagementSystem.Startup))]
namespace StudentDormitoryManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTM1NTYzQDMxMzcyZTMyMmUzMGNwS2F1dUx3QzZZaWw0eFZpVW1sdlhTVUUrU2g0cTlQM3ZZdjlvcURKbms9");
            ConfigureAuth(app);
        }
    }
}
