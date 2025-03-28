using WebApiContaBancaria.Models.Transacoes;
using WebApiContaBancaria.Response.Transacoes;

namespace WebApiContaBancaria.Converters.Transacao {
    public class TransacaoModelToTransacaoResponse {

        public TransacaoResponse Convert(TransacoesModel transacaoModel) {
            return new TransacaoResponse(
                transacaoModel.IdContaOrigem,
                transacaoModel.IdContaDestino,
                transacaoModel.Valor,
                transacaoModel.Tipo,
                transacaoModel.Data
            );
        }
    }
}
