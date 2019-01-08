using System;
using System.Security.Cryptography.X509Certificates;

namespace Vidos.Web.Common.Constants
{
    public static class Constants
    {
        public const string AdministratorName = "Admin";

        public const string AdministratorPassword = "AdminPassword123";

        public const string AdministratorEmail = "kristiangk@abv.bg";

        public const string AdministratorRole = "Admin";

        public const string AdminOrUserRole = AdministratorRole + ", " + UserRole;

        public const string UserRole = "User";

        public const string GuestRole = "Guest";

        public const string AllBrands = "Всички";

        public const string CurrancyType = "bgn";

        public const string ChargeDescription = "Product bought from Vidos shop";

        public const string AdminArea = "Administration";

        public const string Daikin = "Daikin";

        public const string Fujitsu = "Fujitsu";

        public const string Mitsubishi = "Mitsubishi";

        public const string SessionCartKey = "Cart";

        public const string ShoppingArea = "Shopping";

        public const int CentsInLev = 100;

        public const int MinFirstNameLength = 3;
        public const int MaxFirstNameLength = 100;

        public const int MaxLastNameLength = 100;
        public const int MinLastNameLength = 3;

        public const int MaxPasswordLength = 100;
        public const int MinPasswordLength = 3;

        public const int ProductsPerRow = 3;
        public const int MaxItemsPerRow = 12;

        public const int HomeIndexProductCount = 6;

        public const int DescriptionMaxLength = 30;

        public const int MaxRating = 5;

        public const int MinRating = 1;

        public const int ReviewBodyMaxSize = 500;

        public const int ReviewBodyMinSize = 6;

        public static TimeSpan SessionIdleTimeoutTimespan = TimeSpan.FromHours(4);
    }
}
