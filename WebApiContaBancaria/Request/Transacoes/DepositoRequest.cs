using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiContaBancaria.Request.Transacoes {
    public class DepositoRequest {
        public decimal Valor { get; set; }
    }
}
