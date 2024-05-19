using System.ComponentModel.DataAnnotations;

namespace FerryBookingMAUI.Helpers
{
    public class ValidatorHelper
    {
        public static bool TryValidateObject<T>(T obj, out List<ValidationResult> validationResults)
        {
            var validationContext = new ValidationContext(obj);
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}