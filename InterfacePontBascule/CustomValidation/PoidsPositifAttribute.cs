using System.ComponentModel.DataAnnotations;

namespace InterfacePontBascule.CustomValidation
{
    public class PoidsPositifAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int number = (int)value;
            if (number == 0)
            {
                return new ValidationResult("Le poids ne doit pas être inférieur ou égal à 0");
            }
         
            
                return new ValidationResult("Le poids ne doit pas être inférieur ou égal à 0");
            
        }
    }
}
