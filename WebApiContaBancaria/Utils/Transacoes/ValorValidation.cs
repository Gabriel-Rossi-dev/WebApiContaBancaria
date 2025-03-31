using System.ComponentModel.DataAnnotations;

namespace WebApiContaBancaria.Utils.Transacoes {
    public class Valor : ValidationAttribute {

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext) {
            
            var valor = value as decimal?;

            if(valor == null) {
                return new ValidationResult("O Valor é obrigatório");
            }
            if (valor <= 0) {
                return new ValidationResult("O Valor deve ser maior que zero");
            }

            return ValidationResult.Success;
        }
    }
}
