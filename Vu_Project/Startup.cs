using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Vu_Project.Models;

[assembly: OwinStartupAttribute(typeof(Vu_Project.Startup))]
namespace Vu_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createUser();
        }
        
        protected void createUser()
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            
            if(!roleManager.RoleExists(VU.Admin))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();

                role.Name = VU.Admin;

                roleManager.Create(role);
                
                var user = new ApplicationUser();

                user.UserName = "admin@gmail.com";
                user.Email = "admin@gmail.com";
                string UserPwd = "A@Z200711";

                var checkUser = userManager.Create(user, UserPwd);

                if(checkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, VU.Admin);
                }
            }
        }
    }
}
