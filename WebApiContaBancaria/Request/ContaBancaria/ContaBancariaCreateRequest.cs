using System.ComponentModel.DataAnnotations;
using WebApiContaBancaria.Utils.ContaBancaria;


namespace WebApiContaBancaria.Request.ContaBancaria {
    public class ContaBancariaCreateRequest {

        [Required]
        [Cnpj]
        public string Cnpj { get; set; } = string.Empty;

        [Required]
        [NumeroConta]
        public string NumeroConta { get; set; }

        [Required]
        [Agencia]
        public string Agencia { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Banco é obrigatório")]
        public string Banco { get; set; } = string.Empty;

        [Required]
        [Base64]
        public string ImageBase64 { get; set; }

    }
}
