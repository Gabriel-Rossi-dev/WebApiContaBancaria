using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiContaBancaria.Models.Transacoes {
    public class TransacoesDto {

        [ForeignKey("ContaBancaria")]
        public int IdContaOrigem { get; set; }

        [ForeignKey("ContaBancaria")]
        public int IdContaDestino { get; set; }

        public decimal Valor { get; set; }

        public string tipo { get; set; } = string.Empty;
    }
}
