using WebApiContaBancaria.Models.ContaBancariaModel;
using WebApiContaBancaria.Models.Response;
using WebApiContaBancaria.Request.ContaBancaria;
using WebApiContaBancaria.Response.ContaBancaria;

namespace WebApiContaBancaria.Services.ContaBancaria {
    public interface IContaBancariaInterface {

        Task<ResponseModel<List<ContaBancariaResponse>>> GetContasBancarias();

        Task<ResponseModel<ContaBancariaResponse>> GetContaPorId(int idContaBancaria);

        Task<ResponseModel<List<ContaBancariaResponse>>> CriarContaBancaria(ContaBancariaCreateRequest contaBancariaCreateRequest);

        Task<ResponseModel<ContaBancariaResponse>> AtualizarContaBancaria(ContaBancariaUpdateRequest contaBancariaUpdateRequest, int id);

        Task<ResponseModel<List<ContaBancariaResponse>>> ApagarContaBancaria(int id);

    }
}
