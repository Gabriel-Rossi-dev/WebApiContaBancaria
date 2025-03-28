using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using WebApiContaBancaria.Models.ContaBancariaModel;
using WebApiContaBancaria.Models.Response;
using WebApiContaBancaria.Request.ContaBancaria;
using WebApiContaBancaria.Response.ContaBancaria;
using WebApiContaBancaria.Services.ContaBancaria;

namespace WebApiContaBancaria.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ContaBancariaController : ControllerBase {

        private readonly IContaBancariaInterface _contaBancariaInterface;

        private readonly string _diretorioImagens = Path.Combine(Directory.GetCurrentDirectory(), "Imagens");


        public ContaBancariaController(IContaBancariaInterface contaBancariaInterface) {

            _contaBancariaInterface = contaBancariaInterface;
            if (!Directory.Exists(_diretorioImagens)) {
                Directory.CreateDirectory(_diretorioImagens);
            }

        }

        [HttpGet("contas")]
        public async Task<ActionResult<ResponseModel<List<ContaBancariaResponse>>>> GetContasBancarias() {

            var contasBancarias = await _contaBancariaInterface.GetContasBancarias();

            return Ok(contasBancarias);

        }

        [HttpGet]
        [Route("contas/{id}")]
        public async Task<ActionResult<ResponseModel<ContaBancariaResponse>>> GetContaPorId(int id) {

            var contaBancaria = await _contaBancariaInterface.GetContaPorId(id);
            return Ok(contaBancaria);

        }

        [HttpPost]
        [Route("criarcontabancaria")]
        public async Task<ActionResult<ResponseModel<List<ContaBancariaResponse>>>> CriarContaBancaria(ContaBancariaCreateRequest contaBancariaCreateRequest) {

            var contaBancaria = await _contaBancariaInterface.CriarContaBancaria(contaBancariaCreateRequest);
            return Ok(contaBancaria);

        }

        [HttpPut]
        [Route("atualizarcontabancaria/{id}")]
        public async Task<ActionResult<ResponseModel<ContaBancariaResponse>>> AtualizarContaBancaria(ContaBancariaUpdateRequest contaBancariaUpdateRequest, int id) {

            var contaBancaria = await _contaBancariaInterface.AtualizarContaBancaria(contaBancariaUpdateRequest, id);
            return Ok(contaBancaria);

        }

        [HttpDelete]
        [Route("apagarcontabancaria/{id}")]
        public async Task<ActionResult<ResponseModel<ContaBancariaResponse>>> ApagarContaBancaria( int id) {

            var contaBancaria = await _contaBancariaInterface.ApagarContaBancaria(id);
            return Ok(contaBancaria);
        }

    }
}
