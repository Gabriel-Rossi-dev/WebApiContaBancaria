namespace WebApiContaBancaria.Response.ContaBancaria {
    public class ContaBancariaResponse {

        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Cnpj { get; set; } = string.Empty;

        public string NumeroConta { get; set; } = string.Empty;

        public string Agencia { get; set; } = string.Empty;

        public ContaBancariaResponse(int id, string nome, string cnpj, string numeroConta, string agencia) {
            Id = id;
            Nome = nome;
            Cnpj = cnpj;
            NumeroConta = numeroConta;
            Agencia = agencia;
        }
    }
}
