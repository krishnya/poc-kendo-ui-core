//using IFMAMVCDemo.Data.Models;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace IFMAMVCDemo.Data
//{
//    public class DbInitializer
//    {
//        public static async Task Initialize(ApplicationDbContext context, IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
//        {
//            //context.Database.EnsureCreated();           
//            SeedCategories(context);
//            SeedTitles(context);
//            await SeedInitialUser(userManager, context);

//        }

//        private static async Task SeedInitialUser(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
//        {
//            if (context.Users.Any())
//            {
//                return;
//            }
//            ApplicationUser user = new ApplicationUser()
//            {
//                Id = Guid.NewGuid().ToString(),
//                UserName = "krishna",
//                //FullName = "Jaxons Danniels",
//                Email = "krishna@company.com",
//                //Company = "Progress",
//                LockoutEnabled = false,
//                PhoneNumber = "112345678901",
//                PhoneNumberConfirmed = true,
//                EmailConfirmed = true,
//                TwoFactorEnabled = false
//            };

//            var result = await userManager.CreateAsync(user);
//            if (result.Succeeded)
//            {
//                await userManager.AddPasswordAsync(user, "User*123");
//            }

//            context.SaveChanges();

//        }

//         //Add a SeedCategories method to seed data with categories Category-A amount 5000, Category-B amount 7000, Category-C amount 10000
//        private static void SeedCategories(ApplicationDbContext context)
//        {
//            if (context.Categories.Any())
//            {
//                return;
//            }

//            var categories = new List<Category>
//            {
//                new Category { CategoryName = "Category-A", Amount = 5000 },
//                new Category { CategoryName = "Category-B", Amount = 7000 },
//                new Category { CategoryName = "Category-C", Amount = 10000 }
//            };

//            context.Categories.AddRange(categories);
//            context.SaveChanges();
//        }

//        //Add a SeedTitles method to see data with titles Manager CategoryId 2, Assistant CategoryId 1, Director CategoryId 3, Supervisor CategoryId 2, Engineer CategoryId 1, Technician CategoryId 1, Specialist CategoryId 2, Analyst CategoryId 1, Designer CatB, Administrator CategoryId 3
//        private static void SeedTitles(ApplicationDbContext context)
//        {
//            if (context.Titles.Any())
//            {
//                return;
//            }

//            var titles = new List<Title>
//            {
//                new Title { TitleName = "Manager", CategoryId = 2 },
//                new Title { TitleName = "Assistant", CategoryId = 1 },
//                new Title { TitleName = "Director", CategoryId = 3 },
//                new Title { TitleName = "Supervisor", CategoryId = 2 },
//                new Title { TitleName = "Engineer", CategoryId = 1 },
//                new Title { TitleName = "Technician", CategoryId = 1 },
//                new Title { TitleName = "Specialist", CategoryId = 2 },
//                new Title { TitleName = "Analyst", CategoryId = 1 },
//                new Title { TitleName = "Designer", CategoryId = 2 },
//                new Title { TitleName = "Administrator", CategoryId = 3 }
//            };

//            context.Titles.AddRange(titles);
//            context.SaveChanges();
//        }


//    }
//}
