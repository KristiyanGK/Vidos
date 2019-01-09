using Vidos.Services.Mapping;

namespace Vidos.Services.Models.Brand.ViewModels
{
    public class BrandDetailsViewModel : IMapFrom<Data.Models.Brand>
    {
        public string Name { get; set; }

        public string Information { get; set; }

        public string LogoPath { get; set; }
    }
}
