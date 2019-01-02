using System;
using System.Linq;
using Vidos.Data.Models;
using Vidos.Services.DataServices.Enums;
using Vidos.Web.Common.Constants;

namespace Vidos.Services.DataServices.Extensions
{
    public static class QuerableProductsExtension
    {
        public static IQueryable<AirConditioner> ApplyFilters(this IQueryable<AirConditioner> source ,string brandName, string priceSort)
        {
            bool filterBrand = !string.IsNullOrEmpty(brandName)
                                && !string.IsNullOrWhiteSpace(brandName)
                                && brandName != Constants.AllBrands;

            if (filterBrand)
            {
                source = source.Where(p => p.Brand.Name == brandName);
            }

            bool hasParsed = Enum.TryParse(priceSort, out ProductPriceSortEnums sort);

            if (hasParsed)
            {
                switch (sort)
                {
                    case ProductPriceSortEnums.Ascending:
                        source = source.OrderBy(p => p.Price);
                        break;
                    case ProductPriceSortEnums.Descending:
                        source = source.OrderByDescending(p => p.Price);
                        break;
                }
            }

            return source;
        }
    }
}
