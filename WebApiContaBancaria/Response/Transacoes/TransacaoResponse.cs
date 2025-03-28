using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiContaBancaria.Response.Transacoes {
    public class TransacaoResponse {

        public int IdContaOrigem { get; set; }

        public int IdContaDestino { get; set; }

        public decimal Valor { get; set; }

        public string Tipo { get; set; } = string.Empty;

        public DateTime Data { get; set; }

        public TransacaoResponse(int idContaOrigem, int idContaDestino, decimal valor, string tipo, DateTime data) {
            IdContaOrigem = idContaOrigem;
            IdContaDestino = idContaDestino;
            Valor = valor;
            Tipo = tipo;
            Data = data;
        }
    }
}
