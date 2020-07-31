using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Proyecto_PAA.App_Start.Startup))]

namespace Proyecto_PAA.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions {
                LoginPath = new PathString("/Auth/Login"),
                ExpireTimeSpan= TimeSpan.FromMinutes(30),
                AuthenticationType= DefaultAuthenticationTypes.ApplicationCookie
            });
            // Para obtener más información sobre cómo configurar la aplicación, visite https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
