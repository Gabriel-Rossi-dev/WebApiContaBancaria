using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApiContaBancaria.Utils {
    public class Agencia : ValidationAttribute {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

            var agencia = value as string;

            if (string.IsNullOrEmpty(agencia)) {
                return new ValidationResult("A Agencia é obrigatória");
            }

            if (!Regex.IsMatch(agencia, @"^\d+$")) {
                return new ValidationResult("A Agencia deve conter somente números");
            }

            agencia = Regex.Replace(agencia, @"[^\d]", "");

            if (agencia.Length != 4) {
                return new ValidationResult("A Agencia deve conter 4 digitos");
            }

            return ValidationResult.Success;
        }
    }
}
