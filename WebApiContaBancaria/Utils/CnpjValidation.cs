using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace WebApiContaBancaria.Utils {
    public class Cnpj :ValidationAttribute {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

            var cnpj = value as string;

            if (string.IsNullOrEmpty(cnpj)) {
                return new ValidationResult("O CNPJ é obrigatório.");
            }


            if (!Regex.IsMatch(cnpj, @"^\d+$")) {
                return new ValidationResult("O CNPJ deve conter somente números");
            }

            cnpj = Regex.Replace(cnpj, @"[^\d]", "");

            if (cnpj.Length != 14) {
                return new ValidationResult("O Cnpj deve conter 14 dígitos");
            }

            return ValidationResult.Success;

        }
    }
}
