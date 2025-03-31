using System.ComponentModel.DataAnnotations;
using WebApiContaBancaria.Utils.Transacoes;

namespace WebApiContaBancaria.Request.Transacoes {
    public class TransferenciaRequest {

        [Required(ErrorMessage = "É preciso informar a conta de destino")]
        public int IdContaDestino { get; set; }

        [Required]
        [Valor]
        public decimal Valor { get; set; }

    }
}
