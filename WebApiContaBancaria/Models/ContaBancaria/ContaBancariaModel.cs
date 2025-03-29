using System.ComponentModel.DataAnnotations;

namespace WebApiContaBancaria.Models.ContaBancariaModel {
    public class ContaBancariaModel {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Cnpj { get; set; } = string.Empty;

        public string NumeroConta { get; set; }

        public string Agencia { get; set; } = string.Empty;

        public string Banco { get; set; } = string.Empty;

        public string ImageBase64  { get; set; }

        public bool Ativo { get; set; }

        public ContaBancariaModel(string nome, string cnpj, string numeroConta, string agencia, string banco, string imageBase64) {
            Nome = nome;
            Cnpj = cnpj;
            NumeroConta = numeroConta;
            Agencia = agencia;
            Banco = banco;
            ImageBase64 = imageBase64;
            Ativo = true;
        }
    }
}
