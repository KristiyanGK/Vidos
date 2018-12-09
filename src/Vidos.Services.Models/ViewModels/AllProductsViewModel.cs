using Vidos.Data.Models;
using Vidos.Services.Mapping;

namespace Vidos.Services.Models.ViewModels
{
    public class AllProductsViewModel : IMapFrom<AirConditioner>
    {
        private const int DescriptionMaxLength = 30;

        private string description;

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImagePath { get; set; }

        public string Description
        {
            get => this.description;

            set
            {
                if (value.Length >= DescriptionMaxLength)
                {
                    value = value.Substring(0, DescriptionMaxLength) + "...";
                }

                this.description = value;
            }
        }
    }
}
