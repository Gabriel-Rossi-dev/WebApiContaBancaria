using System.ComponentModel.DataAnnotations;

namespace WebApiContaBancaria.Models.ContaBancariaModel {
    public class ContaBancariaModel {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Cnpj { get; set; } = string.Empty;

        public string NumeroConta { get; set; }

        public string Agencia { get; set; } = string.Empty;

        public string Documento  { get; set; }

        public bool Ativo { get; set; }

        public ContaBancariaModel(string nome, string cnpj, string numeroConta, string agencia, string documento) {
            Nome = nome;
            Cnpj = cnpj;
            NumeroConta = numeroConta;
            Agencia = agencia;
            Documento = documento;
            Ativo = true;
        }
    }
}
