using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace WebApiContaBancaria.Models.Transacoes {
    public class TransacoesModel {

        [Key]
        public int Id { get; set; }

        [ForeignKey("ContaBancaria")]
        public int IdContaOrigem { get; set; }

        [ForeignKey("ContaBancaria")]
        public int IdContaDestino { get; set; }

        public decimal Valor { get; set; }

        //"Deposito", "Saque", "Transferencia"
        public string Tipo { get; set; } = string.Empty;

        public DateTime Data { get; set; }

        public TransacoesModel(int idContaOrigem, int idContaDestino, decimal valor, string tipo, DateTime data) {
            IdContaOrigem = idContaOrigem;
            IdContaDestino = idContaDestino;
            Valor = valor;
            Tipo = tipo;
            Data = data;
        }
    }
}
