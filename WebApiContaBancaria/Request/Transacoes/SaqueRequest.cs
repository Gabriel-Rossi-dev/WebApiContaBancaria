using System.ComponentModel.DataAnnotations;
using WebApiContaBancaria.Utils.Transacoes;

namespace WebApiContaBancaria.Request.Transacoes {
    public class SaqueRequest {

        [Required]
        [Valor]
        public decimal Valor { get; set; }
    }
}
