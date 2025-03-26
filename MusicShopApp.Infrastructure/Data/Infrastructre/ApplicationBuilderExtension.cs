using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using MusicShopApp.Data;
using MusicShopApp.Infrastructure.Data.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicShopApp.Infrastructure.Data.Infrastructre
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            await RoleSeeder(services);
            await SeedAdministrator(services);

            var dataCategory = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedCategories(dataCategory);

            var dataBrand = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedBrands(dataBrand);

            return app;
        }

        private static void SeedCategories(ApplicationDbContext dataCategory)
        {
            if(dataCategory.Categories.Any())
            {
                return;
            }
            dataCategory.Categories.AddRange(new[]
            {
                new Category{CategoryName = "CD"},
                new Category{CategoryName = "Vynil"}
            }
            );
            dataCategory.SaveChanges();
        }

        private static void SeedBrands (ApplicationDbContext dataBrand)
        {
            if(dataBrand.Brands.Any())
            {  return;}

            dataBrand.Brands.AddRange(new[]

            {

                new Brand{BrandName = "Pop"},


                new Brand{BrandName = "Metal" },

                new Brand{BrandName = "Rap" },
                new Brand{BrandName = "Classic" },
                new Brand{BrandName = "Rock" },


            }
                );
            dataBrand.SaveChanges();
        }

        private static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Administrator", "Client" };
            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task SeedAdministrator(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByNameAsync("admin") == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    FirstName = "admin",
                    LastName = "admin",
                    UserName = "admin",
                    Email = "admin@admin.com",
                    Address = "admin address",
                    PhoneNumber = "0888888888"
                };

                var result = await userManager.CreateAsync(user, "Admin123456");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

    }

}
