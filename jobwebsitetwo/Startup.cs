using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication1.Models;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            adduserandrole();
        }
        public void adduserandrole()
        {
            var rolemaneger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var usermaneger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            if (!rolemaneger.RoleExists("admins"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "admins";
                rolemaneger.Create(role);

                ApplicationUser user = new ApplicationUser();
                user.UserName = "amrrizk";
                user.Email = "gdgdg@gmail.com";
                var result = usermaneger.Create(user, "60milion");
                if (result.Succeeded)
                {
                    usermaneger.AddToRole(user.Id, "admins");
                }
            }
        }
    }
}
