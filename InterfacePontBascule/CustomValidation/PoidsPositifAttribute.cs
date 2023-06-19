using System.ComponentModel.DataAnnotations;

namespace InterfacePontBascule.CustomValidation
{
    public class PoidsPositifAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var parc = (Achat)validationContext.ObjectInstance;
            //int number = parc.PCC;

            // string number = value.ToString();
            // if (number.Contains("aa"))

            int number = Convert.ToInt32(value);
            if (number > 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Cette valeur ne doit pas être inférieure ou égal à 0");


        }
    }
}
