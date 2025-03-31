using WebApiContaBancaria.Models.Response;
using WebApiContaBancaria.Request.ContaBancaria;
using WebApiContaBancaria.Response.ContaBancaria;

namespace WebApiContaBancaria.Services.ContaBancaria {
    public interface IContaBancariaInterface {

        Task<ResponseModel<List<ContaBancariaResponse>>> GetContasBancarias();

        Task<ResponseModel<ContaBancariaResponse>> GetContaPorId(int idContaBancaria);

        Task<ResponseModel<ContaBancariaResponse>> CriarContaBancaria(ContaBancariaCreateRequest contaBancariaCreateRequest);

        Task<ResponseModel<ContaBancariaResponse>> AtualizarContaBancaria(ContaBancariaUpdateRequest contaBancariaUpdateRequest, int id);

        Task<ResponseModel<ContaBancariaResponse>> ApagarContaBancaria(int id);

    }
}
