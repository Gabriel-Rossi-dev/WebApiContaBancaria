namespace WebApiContaBancaria.Response.ContaBancaria {
    public class ContaBancariaResponse {

        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Cnpj { get; set; } = string.Empty;

        public string NumeroConta { get; set; } = string.Empty;

        public string Agencia { get; set; } = string.Empty;

        public string Banco { get; set; } = string.Empty;

        // Para Base64
        public string ImageBase64 { get; set; }

        public ContaBancariaResponse(int id, string nome, string cnpj, string numeroConta, string agencia, string banco, string imagemBase64) {
            Id = id;
            Nome = nome;
            Cnpj = cnpj;
            NumeroConta = numeroConta;
            Agencia = agencia;
            Banco = banco;
            ImageBase64 = imagemBase64;
        }
    }
}
