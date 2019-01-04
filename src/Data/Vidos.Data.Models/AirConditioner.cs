using System.Collections.Generic;

namespace Vidos.Data.Models
{
    public class AirConditioner : BaseModel
    {
        public AirConditioner()
        {
            this.Reviews = new HashSet<Review>();
        }

        public decimal Price { get; set; }

        public string Name { get; set; }

        public string BrandId { get; set; }

        public Brand Brand { get; set; }

        public string ImagePath { get; set; }

        public string Description { get; set; }

        public string Origin { get; set; }

        public double Cooling { get; set; } // kW

        public double Heating { get; set; } // kW

        public double HeatingConsumption { get; set; } // kW

        public double CoolingConsumption { get; set; } // kW

        public ICollection<Review> Reviews { get; set; }
    }
}
