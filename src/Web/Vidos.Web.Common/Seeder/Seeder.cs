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
            var adminRoleExists = _roleManager.RoleExistsAsync(Constants.AdministratorRole).Result;
            if (!adminRoleExists)
            {
                _roleManager.CreateAsync(new IdentityRole { Name = Constants.AdministratorRole }).GetAwaiter().GetResult();
            }

            var userRoleExists = _roleManager.RoleExistsAsync(Constants.UserRole).Result;
            if (!userRoleExists)
            {
                _roleManager.CreateAsync(new IdentityRole { Name = Constants.UserRole }).GetAwaiter().GetResult();
            }
        }

        public void SeedAdmin()
        {
            var adminUserExists = _userManager.FindByEmailAsync(Constants.AdministratorEmail).Result != null;

            if (!adminUserExists)
            {
                var user = new VidosUser
                {
                    UserName = Constants.AdministratorEmail,
                    Email = Constants.AdministratorEmail,
                    FirstName = Constants.AdministratorName,
                    LastName = Constants.AdministratorName,
                };

                _userManager.CreateAsync(user, Constants.AdministratorPassword).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, Constants.AdministratorRole).GetAwaiter().GetResult();
            }
        }

        public void SeedProducts()
        {
            if (_context.AirConditioners.Count() != 0)
            {
                return;
            }

            Brand[] brands =
            {
                new Brand { Name = "Daikin"},
                new Brand { Name = "Mitsubishi"},
                new Brand { Name = "Fujitsu"},
                new Brand { Name = "Toshiba"}
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
                    Price = 100M + i,
                    Name = "SeededAC" + i,
                    Brand = brands[i % brands.Length],
                    Description = string.Concat("Description ", "This is a continuation " + new string('*', i)),
                    Origin = origins[i % origins.Length],
                    Cooling = 97D + i,
                    Heating = 96D + i,
                    CoolingConsumption = 98D + i,
                    HeatingConsumption = 99D + i,
                    ImagePath = imagePaths[i % imagePaths.Length]
                };

                _context.AirConditioners.Add(ac);
            }

            _context.SaveChanges();
        }
    }
}
