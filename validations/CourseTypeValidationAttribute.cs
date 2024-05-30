using academ_sync_back.Enums;
using System.ComponentModel.DataAnnotations;

namespace academ_sync_back.validations
{
    public class CourseTypeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string roleValue = value.ToString();
                if (!Enum.TryParse(typeof(CourseType), roleValue, true, out _))
                {
                    return new ValidationResult("Invalid CourseType value.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
