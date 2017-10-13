using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
using PostgreSQL.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using FreneValue.Models;

using Owin;


[assembly: OwinStartupAttribute(typeof(FreneValue.Startup))]
namespace FreneValue
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        // In this method we will create default User roles and Admin user for login   
        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                // var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                var role = new PostgreSQL.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "iaer";
                user.Email = "simocl@yaoo.fr";

                string userPWD = "Dieri@@@&2016";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            //// creating Creating Manager role    
            //if (!roleManager.RoleExists("Manager"))
            //{
            //    //var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            //    var role = new PostgreSQL.AspNet.Identity.EntityFramework.IdentityRole();
            //    role.Name = "Manager";
            //    roleManager.Create(role);

            //}

            // creating Creating Employee role    
            if (!roleManager.RoleExists("Employee"))
            {
                //var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                var role = new PostgreSQL.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);

            }

            // creating Creating Employee role    
            if (!roleManager.RoleExists("Citoyen"))
            {
                //var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                var role = new PostgreSQL.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Citoyen";
                roleManager.Create(role);

            }

            //// creating Creating Employee role    
            //if (!roleManager.RoleExists("Municipalite"))
            //{
            //    //var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            //    var role = new PostgreSQL.AspNet.Identity.EntityFramework.IdentityRole();
            //    role.Name = "Municipalite";
            //    roleManager.Create(role);

            //}

            //// creating Creating Employee role    
            //if (!roleManager.RoleExists("Institution"))
            //{
            //    //var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            //    var role = new PostgreSQL.AspNet.Identity.EntityFramework.IdentityRole();
            //    role.Name = "Institution";
            //    roleManager.Create(role);

            //}
        }
    }
}


