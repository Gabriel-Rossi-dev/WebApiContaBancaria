using System.ComponentModel.DataAnnotations;
using WebApiContaBancaria.Utils;


namespace WebApiContaBancaria.Request.ContaBancaria {
    public class ContaBancariaCreateRequest {

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;

        [Cnpj]
        public string Cnpj { get; set; } = string.Empty;

        [NumeroConta]
        public int NumeroConta { get; set; }

        [Agencia]
        public string Agencia { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Banco é obrigatório")]
        public string Banco { get; set; } = string.Empty;

        [Base64]
        public string ImageBase64 { get; set; }

    }
}
