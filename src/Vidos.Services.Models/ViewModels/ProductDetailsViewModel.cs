using Vidos.Data.Models;
using Vidos.Services.Mapping;

namespace Vidos.Services.Models.ViewModels
{
    public class ProductDetailsViewModel : IMapFrom<AirConditioner>
    {
        public string ImagePath { get; set; }

        public string Description { get; set; }

        public string BrandName { get; set; }

        public decimal Price { get; set; }

        public string Origin { get; set; }

        public double Cooling { get; set; } // kW

        public double Heating { get; set; } // kW

        public double HeatingConsumption { get; set; } // kW

        public double CoolingConsumption { get; set; } // kW
    }
}
