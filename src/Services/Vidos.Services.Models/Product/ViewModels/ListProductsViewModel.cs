using Vidos.Data.Models;
using Vidos.Services.Mapping;
using Vidos.Web.Common.Constants;

namespace Vidos.Services.Models.Product.ViewModels
{
    public class ListProductsViewModel : IMapFrom<AirConditioner>
    {
        private string description;

        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImagePath { get; set; }

        public string Description
        {
            get => this.description;

            set
            {
                if (value.Length >= Constants.DescriptionMaxLength)
                {
                    value = value.Substring(0, Constants.DescriptionMaxLength) + "...";
                }

                this.description = value;
            }
        }
    }
}
