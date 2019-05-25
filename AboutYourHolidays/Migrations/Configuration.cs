namespace AboutYourHolidays.Migrations
{
    using AboutYourHolidays.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AboutYourHolidays.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            // Do debugowania metody seed
            // if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();

            SeedRoles(context);
            SeedUsers(context);
            SeedPosts(context);
            SeedComments(context);
        }
        private void SeedRoles(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>());
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
        }
        private void SeedUsers(ApplicationDbContext context)
        {
            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store);
            if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                var user = new User { UserName = "Admin" };

                var adminresult = manager.Create(user, "12345678");
                if (adminresult.Succeeded)
                    manager.AddToRole(user.Id, "Admin");
            }
        }
        private void SeedPosts(ApplicationDbContext context)
        {
            var idUzytkownika = context.Set<User>().Where(u => u.UserName == "Admin").FirstOrDefault().Id;
            for (int i = 1; i <= 10; i++)
            {
                var ogl = new Post()
                {
                    Id = i,
                    UserId = idUzytkownika,
                    Tilte = "Treœæ og³oszenia" + i.ToString(),
                    Description = "Tytu³ og³oszenia" + i.ToString(),
                    CreatedOn = DateTime.Now.AddDays(-i),
                };
                context.Set<Post>().AddOrUpdate(ogl);
            }
            context.SaveChanges();
        }
        private void SeedComments(ApplicationDbContext context)
        {
            var idUzytkownika = context.Set<User>().Where(u => u.UserName == "Admin").FirstOrDefault().Id;
            for (int i = 1; i <= 10; i++)
            {
                var kat = new Comment()
                {
                    Id = i,
                    Text = "texttttt" + i.ToString(),
                    PostId = 1,
                    UserId = idUzytkownika,
                    CreatedOn = DateTime.Now.AddDays(-i),
                    LastUpdatedOn = DateTime.Now.AddDays(-i)
                };
                context.Set<Comment>().AddOrUpdate(kat);
            }
            context.SaveChanges();
        }
    }
}