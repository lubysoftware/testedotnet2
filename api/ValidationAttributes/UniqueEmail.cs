using Infra;
using Persistencia;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace api.ValidationAttributes
{
    public class UniqueEmail : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (Context)validationContext.GetService(typeof(Context));
            if (value != null && !string.IsNullOrEmpty(""+value) )
            {
                if (service.Developers.Where(d => d.Email.CompareTo(value.ToString()) == 0).Count() == 0)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
