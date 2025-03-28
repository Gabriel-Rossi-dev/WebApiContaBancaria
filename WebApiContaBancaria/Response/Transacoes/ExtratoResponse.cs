using WebApiContaBancaria.Models.Transacoes;

namespace WebApiContaBancaria.Response.Transacoes {
    public class ExtratoResponse {

        public List<TransacoesModel>? Extrato { get; set; }
        public decimal Saldo { get; set; }

    }
}
