﻿using System;

namespace Vidos.Web.Common.Constants
{
    public static class Constants
    {
        public const string AdministratorName = "Admin";

        public const string AdministratorPassword = "AdminPassword123";

        public const string AdministratorEmail = "kristiangk@abv.bg";

        public const string AdministratorRole = "Admin";

        public const string UserRole = "User";

        public const int MinFirstNameLength = 3;
        public const int MaxFirstNameLength = 100;

        public const int MaxLastNameLength = 100;
        public const int MinLastNameLength = 3;

        public const int MaxPasswordLength = 100;
        public const int MinPasswordLength = 3;

        public const int ProductsPerRow = 3;
        public const int MaxItemsPerRow = 12;

        public static TimeSpan SessionIdleTimeoutTimespan = TimeSpan.FromHours(4);
    }
}