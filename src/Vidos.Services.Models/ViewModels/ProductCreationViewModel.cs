using Vidos.Data.Models;
using Vidos.Services.Mapping;

namespace Vidos.Services.Models.ViewModels
{
    //TODO Add Validation!

    public class ProductCreationViewModel : IMapTo<AirConditioner>
    {
        public decimal Price { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public string ImagePath { get; set; }

        public string Description { get; set; }

        public string Origin { get; set; }

        public double Cooling { get; set; } // kW

        public double Heating { get; set; } // kW

        public double HeatingConsumption { get; set; } // kW

        public double CoolingConsumption { get; set; } // kW
    }
}
