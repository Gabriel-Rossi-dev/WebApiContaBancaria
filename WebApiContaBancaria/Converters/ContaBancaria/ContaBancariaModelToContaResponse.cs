using WebApiContaBancaria.Models.ContaBancariaModel;
using WebApiContaBancaria.Response.ContaBancaria;

namespace WebApiContaBancaria.Converters.ContaBancaria {
    public class ContaBancariaModelToContaResponse {


        public ContaBancariaResponse Convert(ContaBancariaModel contaBancariaModel) {
            return new ContaBancariaResponse(
                contaBancariaModel.Id,
                contaBancariaModel.Nome,
                contaBancariaModel.Cnpj,
                contaBancariaModel.NumeroConta,
                contaBancariaModel.Agencia,
                contaBancariaModel.Banco,
                contaBancariaModel.ImageBase64
            );
        }

    }
}
