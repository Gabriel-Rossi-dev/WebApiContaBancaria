using System.ComponentModel.DataAnnotations;
using WebApiContaBancaria.Utils;


namespace WebApiContaBancaria.Request.ContaBancaria {
    public class ContaBancariaCreateRequest {

        [Cnpj]
        public string Cnpj { get; set; } = string.Empty;

        [NumeroConta]
        public string NumeroConta { get; set; }

        [Agencia]
        public string Agencia { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Banco é obrigatório")]
        public string Banco { get; set; } = string.Empty;

        [Base64]
        public string ImageBase64 { get; set; }

    }
}
