using Vidos.Data.Models;
using Vidos.Services.Mapping;

namespace Vidos.Services.Models
{
    public class AllProductsViewModel : IMapFrom<AirConditioner>
    {
        private string description;

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImagePath { get; set; }

        public string Description
        {
            get => this.description;

            set
            {
                if (value.Length >= 30)
                {
                    value = value.Substring(0, 30) + "...";
                }

                this.description = value;
            }
        }
    }
}
