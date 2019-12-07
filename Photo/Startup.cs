﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Photo.Models;

[assembly: OwinStartupAttribute(typeof(Photo.Startup))]
namespace Photo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createAdminUserAndApplicationRoles();
        }

        private void createAdminUserAndApplicationRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Administrator")) {  
                var role = new IdentityRole();                
                role.Name = "Administrator";       
                roleManager.Create(role); 
                var user = new ApplicationUser();     
                user.UserName = "admin@admin.com";        
                user.Email = "admin@admin.com"; 
                var adminCreated = UserManager.Create(user, "Administrator1!");
                if (adminCreated.Succeeded) { 
                    UserManager.AddToRole(user.Id, "Administrator"); 
                }
            } 
 

            if (!roleManager.RoleExists("User")) {   
                var role = new IdentityRole(); role.Name = "User";   
                roleManager.Create(role);       
            } 
 
        } 
 
    }
}
