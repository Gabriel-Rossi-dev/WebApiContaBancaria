using System.ComponentModel.DataAnnotations;
using WebApiContaBancaria.Utils;

namespace WebApiContaBancaria.Request.ContaBancaria {
    public class ContaBancariaUpdateRequest {

        [NumeroConta]
        public string NumeroConta { get; set; } = string.Empty;

        [Agencia]
        public string Agencia { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Banco é obrigatório")]
        public string Banco { get; set; } = string.Empty;

    }
}
