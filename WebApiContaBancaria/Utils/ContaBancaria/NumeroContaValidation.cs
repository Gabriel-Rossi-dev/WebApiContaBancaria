using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApiContaBancaria.Utils.ContaBancaria {
    public class NumeroConta : ValidationAttribute {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

            var numeroConta = value as string;

            if (string.IsNullOrEmpty(numeroConta)) {
                return new ValidationResult("O Conta é obrigatório");
            }

            if (!Regex.IsMatch(numeroConta, @"^\d+$")) {
                return new ValidationResult("A Conta deve conter apenas números.");
            }

            if (numeroConta.Length < 6 && numeroConta.Length > 12) {
                return new ValidationResult("o Conta deve conter entre 6 e 12 digitos");
            }

            return ValidationResult.Success;

        }
    }
}
