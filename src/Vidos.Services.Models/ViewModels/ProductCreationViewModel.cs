using System.ComponentModel.DataAnnotations;
using Vidos.Data.Models;
using Vidos.Services.Mapping;

namespace Vidos.Services.Models.ViewModels
{
    //TODO Add Validation!

    public class ProductCreationViewModel : IMapTo<AirConditioner>
    {
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Origin { get; set; }

        [Required]
        public double Cooling { get; set; } // kW

        [Required]
        public double Heating { get; set; } // kW

        [Required]
        public double HeatingConsumption { get; set; } // kW

        [Required]
        public double CoolingConsumption { get; set; } // kW
    }
}
