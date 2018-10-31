using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Password_Manager_WebApp.Models.Entities;
using System.Collections.Generic;

namespace Password_Manager_WebApp.Helpers
{
    public class IdentityHelper
    {
        public bool RoleExists(string name)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new DatabaseContext()));
            return roleManager.RoleExists(name);
        }

        public bool CreateRole(string name)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new DatabaseContext()));
            var idResult = roleManager.Create(new IdentityRole(name));
            return idResult.Succeeded;
        }

        public bool CreateUser(ApplicationUser user, string password)
        {
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new DatabaseContext()));
            var idResult = userManager.Create(user, password);
            return idResult.Succeeded;
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new DatabaseContext()));
            var idResult = userManager.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }

        public void ClearUserRoles(string userId)
        {
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(new DatabaseContext()));
            var user = userManager.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();
            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                userManager.RemoveFromRole(userId, role.Role.Name);
            }
        }

    }
}