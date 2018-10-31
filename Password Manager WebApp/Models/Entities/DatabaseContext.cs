using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Password_Manager_WebApp.Models.Entities
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Account> Accounts { get; set; }
    }


    public class ApplicationUser : IdentityUser
    {
    }
}