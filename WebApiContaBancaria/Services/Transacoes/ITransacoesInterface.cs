using WebApiContaBancaria.Models.Response;
using WebApiContaBancaria.Models.Transacoes;
using WebApiContaBancaria.Request.Transacoes;
using WebApiContaBancaria.Response.Transacoes;

namespace WebApiContaBancaria.Services.Transacoes {
    public interface ITransacoesInterface {
        Task<ResponseModel<TransacaoResponse>> Saque(SaqueRequest saqueRequest, int id);
        Task<ResponseModel<TransacaoResponse>> Deposito(DepositoRequest depositoRequest, int id);
        Task<ResponseModel<TransacaoResponse>> Transferencia(TransferenciaRequest transacoesModelDto,int id);
        Task<ResponseModel<ExtratoResponse>> Extrato(int id);
    }
}
