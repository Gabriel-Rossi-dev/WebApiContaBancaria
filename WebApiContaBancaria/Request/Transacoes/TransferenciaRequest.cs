using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiContaBancaria.Request.Transacoes {
    public class TransferenciaRequest {

        public int IdContaDestino { get; set; }

        public decimal Valor { get; set; }

    }
}
