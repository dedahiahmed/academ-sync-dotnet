using academ_sync_back.Enums;
using System.ComponentModel.DataAnnotations;

namespace academ_sync_back.validations
{
    public class SemesterTypeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string roleValue = value.ToString();
                if (!Enum.TryParse(typeof(Semester), roleValue, true, out _))
                {
                    return new ValidationResult("Invalid semester value.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
