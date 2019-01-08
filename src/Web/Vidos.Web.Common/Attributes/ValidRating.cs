using System.ComponentModel.DataAnnotations;
using Vidos.Web.Common.Constants;

namespace Vidos.Web.Common.Attributes
{
    public class ValidRating : ValidationAttribute
    {
        public ValidRating()
        {
            this.ErrorMessage 
                = string.Format(ErrorMessages.InvalidRating,
                    Constants.Constants.MinRating,
                    Constants.Constants.MaxRating);
        }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            bool isRating = (int) value >= Constants.Constants.MinRating 
                            && (int) value <= Constants.Constants.MaxRating;

            return isRating
                ? new ValidationResult(this.ErrorMessage) 
                : ValidationResult.Success;
        }
    }
}
