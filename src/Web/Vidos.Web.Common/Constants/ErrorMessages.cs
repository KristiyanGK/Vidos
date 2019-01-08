using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Vidos.Web.Common.Constants
{
    public static class ErrorMessages
    {
        public const string PasswordMissMatch 
            = "Паролата и паролата за потвърждение са различни!";

        public const string StringInvalidLength 
            = "Дължината на {0} трябва да бъде между {2} и {1} символа.";

        public const string EmptyCart 
            = "Количката Ви е празна!";

        public const string FirstNameRequired 
            = "Моля попълнете име!";

        public const string LastNameRequired 
            = "Моля попълнете фамилия!";

        public const string AddressRequired 
            = "Моля попълнете адреса!";

        public const string CityRequired 
            = "Моля попълнете град!";

        public const string StateRequired 
            = "Моля попълнете област!";

        public const string CountryRequired 
            = "Моля попълнете държава!";

        public const string EmailRequired 
            = "Моля попълнете емайл!";

        public const string InvalidPaymentMethod 
            = "Не е избран валиден метод за плащане";

        public const string InvalidRating 
            = "Оценката трябва да е цяло число между {0} и {1}";

        public const string InvalidReviewBodySize
            = "Текстът трябва да е между {0} и {1} символа";

        public const string NegativeDecimal
            = "Числото трябва да бъде положително";
    }
}
