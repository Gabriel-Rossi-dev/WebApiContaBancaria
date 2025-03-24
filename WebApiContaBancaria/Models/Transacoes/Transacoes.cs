using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiContaBancaria.Models.Transacoes {
    public class Transacoes {

        [Key]
        public int Id { get; set; }

        [ForeignKey("ContaBancaria")]
        public int IdContaOrigem { get; set; }

        [ForeignKey("ContaBancaria")]
        public int IdContaDestino { get; set; }

        public decimal Valor { get; set; }

        public string tipo { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DataType Data { get; set; }
    }
}
