using System.ComponentModel.DataAnnotations;

namespace educational_practice5.Services
{
    // public class ValidEnumValueAttribute : ValidationAttribute
    // {
    //     protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //     {
    //         if (!Enum.GetNames(typeof(Department)).Contains(value.ToString().ToLower()))
    //         {
    //             return new ValidationResult($"Incorrect data. It must be a correct name, not the '{value.ToString()}");
    //         }
    //         return ValidationResult.Success;
    //     }
    // }
    //
    // public class ValidDateValueAttribute : ValidationAttribute
    // {
    //     protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //     {
    //         DateTime d = Convert.ToDateTime(value);
    //         if (d < DateTime.Now) return ValidationResult.Success;
    //         else return new ValidationResult("Incorrect data. Date can`t be grater than present");
    //     }
    // }
    //
    public class RequiredGreaterThanZero : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int i;
            if(value != null && int.TryParse(value.ToString(), out i) && i > 0) return ValidationResult.Success;
            else return new ValidationResult("Incorrect data. Number must be > 0.");
        }
    }
}