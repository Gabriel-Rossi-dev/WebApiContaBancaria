using System.ComponentModel.DataAnnotations;

namespace WebApiContaBancaria.Utils {
    public class Base64 : ValidationAttribute {

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext) {
           
            var base64 = value as string;

            if (base64 == null) {
                return new ValidationResult("O documento do tipo base64 é obrigatório");
            }

            if (!base64.StartsWith("data:image/") && !base64.Contains(";base64,")) {
                return new ValidationResult("Imagem não é do tipo base64");
            }

            return ValidationResult.Success;
        }
    }
}
