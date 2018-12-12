using Vidos.Data.Models;
using Vidos.Services.Mapping;

namespace Vidos.Services.Models.ViewModels
{
    public class ProductDetailsViewModel : IMapFrom<AirConditioner>
    {
        public string ImagePath { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
