using System.ComponentModel.DataAnnotations;
using InterfacePontBascule.Models;

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
            if (number == 0 )
            {
                return new ValidationResult("Cetet valeur ne doit pas être inférieure ou égal à 0");
            }


            return new ValidationResult("Cetet valeur ne doit pas être inférieure ou égal à 0");

        }
    }
}
