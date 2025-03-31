using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApiContaBancaria.Utils.ContaBancaria {
    public class Cnpj : ValidationAttribute {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

            var cnpj = value as string;

            if (string.IsNullOrEmpty(cnpj)) {
                return new ValidationResult("O CNPJ é obrigatório.");
            }

            if (!Regex.IsMatch(cnpj, @"^\d+$")) {
                return new ValidationResult("O CNPJ deve conter apenas números.");
            }

            if (cnpj.Length != 14) {
                return new ValidationResult("O CNPJ deve conter 14 dígitos.");
            }

            return ValidationResult.Success;
        }
    }
}
