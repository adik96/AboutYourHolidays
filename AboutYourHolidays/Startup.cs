using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AboutYourHolidays.Startup))]
namespace AboutYourHolidays
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
