using System.ComponentModel.DataAnnotations;
using Vidos.Web.Common.Constants;

namespace Vidos.Web.Common.Attributes
{
    public class PositiveDecimal : ValidationAttribute
    {
        public PositiveDecimal()
        {
            this.ErrorMessage = ErrorMessages.NegativeDecimal;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool isDecimal = decimal.TryParse(value.ToString(), out decimal result);

            if (isDecimal)
            {
                if (result <= 0)
                {
                    return new ValidationResult(this.ErrorMessage);
                }
            }

            bool isDouble = double.TryParse(value.ToString(), out double doubleResult);

            if (isDouble)
            {
                if (doubleResult <= 0)
                {
                    return new ValidationResult(this.ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
