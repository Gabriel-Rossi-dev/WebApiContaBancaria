using System.ComponentModel.DataAnnotations;

namespace WebApiContaBancaria.Request.ContaBancaria {
    public class ContaBancariaUpdateRequest {

        [Required(ErrorMessage = "O campo NumeroConta é obrigatório")]
        public int NumeroConta { get; set; } = 0;

        [Required(ErrorMessage = "O campo Agencia é obrigatório")]
        public string Agencia { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Banco é obrigatório")]
        public string Banco { get; set; } = string.Empty;

    }
}
