using System.ComponentModel.DataAnnotations;
using WebApiContaBancaria.Utils.ContaBancaria;

namespace WebApiContaBancaria.Request.ContaBancaria {
    public class ContaBancariaUpdateRequest {

        [Required]
        [NumeroConta]
        public string NumeroConta { get; set; } = string.Empty;

        [Required]
        [Agencia]
        public string Agencia { get; set; } = string.Empty;


        [Required(ErrorMessage = "O campo Banco é obrigatório")]
        public string Banco { get; set; } = string.Empty;

    }
}
