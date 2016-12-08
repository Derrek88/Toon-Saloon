using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ToonSaloon.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ToonSaloon.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "ToonSaloon.Web.Models.ApplicationDbContext";
        }

        protected override void Seed(ToonSaloon.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            string[] roleNames = { "Admin", "Employee" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                if (!RoleManager.RoleExists(roleName))
                {
                    roleResult = RoleManager.Create(new IdentityRole(roleName));
                }
            }
            var UserManager = new UserManager<Models.ApplicationUser>(new UserStore<Models.ApplicationUser>(context));
            UserManager.AddToRole("4cbb5a3f-f65d-410b-98bb-dd3b890eadf6", "Admin");
            UserManager.AddToRole("6e2098d3-9e33-4b45-98e9-2c1c6e8b3964", "Admin");
            UserManager.AddToRole("87c01746-125d-46b3-8c41-3231d4f97259", "Admin");
            UserManager.AddToRole("b703e624-cdb3-4709-8e49-20f2e6059490", "Employee");
           

        }
    }
}
