using System.Linq;
using Vidos.Data;
using Vidos.Data.Models;

namespace Vidos.Web.Utilities
{
    public class Seeder
    {
        private readonly VidosContext _context;

        public Seeder(VidosContext context)
        {
            this._context = context;
        }

        public void Seed()
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
