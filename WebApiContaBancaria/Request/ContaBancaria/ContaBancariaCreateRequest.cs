using System.ComponentModel.DataAnnotations;

namespace WebApiContaBancaria.Request.ContaBancaria {
    public class ContaBancariaCreateRequest {

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;

        //aqui
        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$", ErrorMessage = "O CNPJ deve estar no formato XX.XXX.XXX/0001-XX.")]
        public string Cnpj { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo NumeroConta é obrigatório")]
        public int NumeroConta { get; set; }

        [Required(ErrorMessage = "O campo Agencia é obrigatório")]
        public string Agencia { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Banco é obrigatório")]
        public string Banco { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo ImageBase64 é obrigatório")]
        public string ImageBase64 { get; set; }

    }
}
