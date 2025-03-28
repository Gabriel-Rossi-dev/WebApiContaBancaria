namespace WebApiContaBancaria.Response.ContaBancaria {
    public class ContaBancariaResponse {
        public string Nome { get; set; } = string.Empty;

        public string Cnpj { get; set; } = string.Empty;

        public int NumeroConta { get; set; } = 0;

        public string Agencia { get; set; } = string.Empty;

        public string Banco { get; set; } = string.Empty;

        // Para Base64
        public string ImageBase64 { get; set; }

        public ContaBancariaResponse(string nome, string cnpj, int numeroConta, string agencia, string banco, string imagemBase64) {
            Nome = nome;
            Cnpj = cnpj;
            NumeroConta = numeroConta;
            Agencia = agencia;
            Banco = banco;
            ImageBase64 = imagemBase64;
        }
    }
}
