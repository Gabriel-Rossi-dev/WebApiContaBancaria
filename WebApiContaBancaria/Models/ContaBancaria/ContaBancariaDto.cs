namespace WebApiContaBancaria.Models.ContaBancaria {
    public class ContaBancariaDto {

        public string Nome { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public int NumeroConta { get; set; }
        public string Agencia { get; set; } = string.Empty;
        public string Banco { get; set; } = string.Empty;
        public byte[] Imagem { get; set; }
    }
}
