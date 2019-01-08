using System.ComponentModel.DataAnnotations;
using Vidos.Data.Models;
using Vidos.Services.Mapping;
using Vidos.Web.Common.Attributes;
using Vidos.Web.Common.Constants;

namespace Vidos.Services.Models.Product.ViewModels
{
    public class ProductsCreateViewModel : IMapTo<AirConditioner>
    {
        [Required]
        [PositiveDecimal]
        [Display(Name = DisplayNames.Price)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = DisplayNames.ProductName)]
        public string Name { get; set; }

        [Required]
        [Display(Name = DisplayNames.Brand)]
        public string BrandName { get; set; }

        [Required]
        [Display(Name = DisplayNames.ImagePath)]
        public string ImagePath { get; set; }

        [Required]
        [Display(Name = DisplayNames.Description)]
        public string Description { get; set; }

        [Required]
        [Display(Name = DisplayNames.Origin)]
        public string Origin { get; set; }

        [Required]
        [PositiveDecimal]
        [Display(Name = DisplayNames.Cooling)]
        public double Cooling { get; set; } // kW

        [Required]
        [PositiveDecimal]
        [Display(Name = DisplayNames.Heating)]
        public double Heating { get; set; } // kW

        [Required]
        [PositiveDecimal]
        [Display(Name = DisplayNames.HeatingConsumption)]
        public double HeatingConsumption { get; set; } // kW

        [Required]
        [PositiveDecimal]
        [Display(Name = DisplayNames.CoolingConsumption)]
        public double CoolingConsumption { get; set; } // kW
    }
}
