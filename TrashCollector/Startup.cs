using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using TrashCollector.Models;

[assembly: OwinStartupAttribute(typeof(TrashCollector.Startup))]
namespace TrashCollector
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
        }

        private void CreateRoles()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            

            if (!roleManager.RoleExists("Employee"))
            {
                var employeeRole = new IdentityRole();
                employeeRole.Name = "Employee";
                roleManager.Create(employeeRole);
            }
            if(!roleManager.RoleExists("Customer"))
            {
                var customerRole = new IdentityRole();
                customerRole.Name = "Customer";
                roleManager.Create(customerRole);
            }
        }
        private void CreateSuperUser()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = new ApplicationUser();
            user.UserName = "admin";
            user.Email = "testadmin@test.com";
            string userPassword = "admin";
            var checkUser = userManager.Create(user, userPassword);

            if (checkUser.Succeeded)
            {
                var result = userManager.AddToRole(user.Id, "Employee");
            }
        }
    }

   
}
