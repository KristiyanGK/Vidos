using System;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Vidos.Data;
using Vidos.Data.Models;

namespace Vidos.Web.Common.Seeder
{
    public class Seeder
    {
        private readonly VidosContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<VidosUser> _userManager;

        public Seeder(VidosContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<VidosUser> userManager)
        {
            this._context = context;
            this._roleManager = roleManager;
            this._userManager = userManager;
        }

        public void SeedRoles()
        {
            var adminRoleExists = _roleManager.RoleExistsAsync(Constants.Constants.AdministratorRole).Result;
            if (!adminRoleExists)
            {
                _roleManager.CreateAsync(new IdentityRole { Name = Constants.Constants.AdministratorRole }).GetAwaiter().GetResult();
            }

            var userRoleExists = _roleManager.RoleExistsAsync(Constants.Constants.UserRole).Result;
            if (!userRoleExists)
            {
                _roleManager.CreateAsync(new IdentityRole { Name = Constants.Constants.UserRole }).GetAwaiter().GetResult();
            }

            var guestRoleExists = _roleManager.RoleExistsAsync(Constants.Constants.GuestRole).Result;
            if (!guestRoleExists)
            {
                _roleManager.CreateAsync(new IdentityRole { Name = Constants.Constants.GuestRole }).GetAwaiter().GetResult();
            }
        }

        public void SeedAdmin()
        {
            var adminUserExists = _userManager.FindByEmailAsync(Constants.Constants.AdministratorEmail).Result != null;

            if (!adminUserExists)
            {
                var user = new VidosUser
                {
                    UserName = Constants.Constants.AdministratorEmail,
                    Email = Constants.Constants.AdministratorEmail,
                    FirstName = Constants.Constants.AdministratorName,
                    LastName = Constants.Constants.AdministratorName,
                };

                _userManager.CreateAsync(user, Constants.Constants.AdministratorPassword).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, Constants.Constants.AdministratorRole).GetAwaiter().GetResult();
            }
        }

        public void SeedProducts()
        {
            var admin = this._userManager.FindByEmailAsync(Constants.Constants.AdministratorEmail).Result;

            if (_context.AirConditioners.Count() != 0)
            {
                return;
            }

            Brand[] brands =
            {
                new Brand
                {
                    Name = "Daikin",
                    LogoPath = "images/logos/daikin.png",
                    Information = "Temp Info Daikin"
                },
                new Brand
                {
                    Name = "Mitsubishi",
                    LogoPath = "images/logos/mitsubishi.png",
                    Information = "Temp Info Mitsubishi"
                },
                new Brand
                {
                    Name = "Fujitsu",
                    LogoPath = "images/logos/fujitsu.jpg",
                    Information = "Temp Info Fujitsu"
                },
                new Brand
                {
                    Name = "Toshiba",
                    LogoPath = "images/logos/toshiba.gif",
                    Information = "Temp Info Toshiba"
                }
            };

            for (int i = 0; i < brands.Length; i++)
            {
                _context.Brands.Add(brands[i]);
            }

            string[] origins =
            {
                "japan",
                "china",
                "america",
                "germany"
            };

            string[] imagePaths =
            {
                "images/ac1.jpg",
                "images/ac2.jpg"
            };

            for (int i = 0; i < 40; i++)
            {
                AirConditioner ac = new AirConditioner
                {
                    TimesBought = 2 * (i + 1),
                    Price = 1000M + i,
                    Name = "SeededAC" + i,
                    Brand = brands[i % brands.Length],
                    Description = string.Concat("Description ", "This is a continuation " + new string('*', i)),
                    Origin = origins[i % origins.Length],
                    Cooling = 97D + i,
                    Heating = 96D + i,
                    CoolingConsumption = 98D + i,
                    HeatingConsumption = 99D + i,
                    ImagePath = imagePaths[i % imagePaths.Length],
                };

                Random rnd = new Random();

                ac.Reviews.Add(new Review
                {
                    Body = "This is a sample admin review for product with id " + ac.Id,
                    ClientId = admin.Id,
                    Rating = rnd.Next(0, 5)
                });

                _context.AirConditioners.Add(ac);
            }

            _context.SaveChanges();
        }
    }
}
