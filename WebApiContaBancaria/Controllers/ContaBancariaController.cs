using Microsoft.AspNetCore.Mvc;
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
        [ProducesResponseType(typeof(ResponseModel<List<ContaBancariaResponse>>), 200)]
        [ProducesResponseType(typeof(ResponseModel<List<ContaBancariaResponse>>), 404)]
        [EndpointDescription("Método para recuperar todas as contas bancárias cadastradas no sistema.")]
        public async Task<ActionResult<ResponseModel<List<ContaBancariaResponse>>>> GetContasBancarias() {


            var contasBancarias = await _contaBancariaInterface.GetContasBancarias();

            if (contasBancarias.StatusCode == 404) {
                return NotFound(contasBancarias);
            }
            else return Ok(contasBancarias);

        }

        [HttpGet]
        [Route("contas/{id}")]
        [ProducesResponseType(typeof(ResponseModel<ContaBancariaResponse>), 200)]
        [ProducesResponseType(typeof(ResponseModel<ContaBancariaResponse>), 404)]
        [EndpointDescription("Método para recuperar uma conta bancária específica com base no ID fornecido no Path.")]
        public async Task<ActionResult<ResponseModel<ContaBancariaResponse>>> GetContaPorId(int id) {

            var contaBancaria = await _contaBancariaInterface.GetContaPorId(id);

            if (contaBancaria.StatusCode == 404) {
                return NotFound(contaBancaria);
            }
            else return Ok(contaBancaria);

        }

        [HttpPost]
        [Route("criarcontabancaria")]
        [ProducesResponseType(typeof(ResponseModel<List<ContaBancariaResponse>>), 201)]
        [ProducesResponseType(typeof(ResponseModel<List<ContaBancariaResponse>>), 400)]
        [ProducesResponseType(typeof(ResponseModel<List<ContaBancariaResponse>>), 502)]
        [EndpointDescription("Método para cria uma nova conta bancária com base nos dados fornecidos.")]
        public async Task<ActionResult<ResponseModel<List<ContaBancariaResponse>>>> CriarContaBancaria(ContaBancariaCreateRequest contaBancariaCreateRequest) {

            var contaBancaria = await _contaBancariaInterface.CriarContaBancaria(contaBancariaCreateRequest);

            if (contaBancaria.StatusCode == 404) {
                return NotFound(contaBancaria);
            }
            else if (contaBancaria.StatusCode == 400) {
                return BadRequest(contaBancaria);
            }
            else if (contaBancaria.StatusCode == 502) {
                return StatusCode(502, contaBancaria);
            }
            else return Created($"contas/{contaBancaria.Dados.Id}", contaBancaria);
           

        }

        [HttpPut]
        [Route("atualizarcontabancaria/{id}")]
        [ProducesResponseType(typeof(ResponseModel<ContaBancariaResponse>), 200)]
        [ProducesResponseType(typeof(ResponseModel<ContaBancariaResponse>), 404)]
        [ProducesResponseType(typeof(ResponseModel<ContaBancariaResponse>), 400)]
        [EndpointDescription("Método para atualiza uma conta bancária existente com base no ID fornecido no Path e nos dados fornecidos no Body da requisição.")]
        public async Task<ActionResult<ResponseModel<ContaBancariaResponse>>> AtualizarContaBancaria(ContaBancariaUpdateRequest contaBancariaUpdateRequest, int id) {

            var contaBancaria = await _contaBancariaInterface.AtualizarContaBancaria(contaBancariaUpdateRequest, id);

            if (contaBancaria.StatusCode == 404) {
                return NotFound(contaBancaria);
            }
            else if (contaBancaria.StatusCode == 400) {
                return BadRequest(contaBancaria);
            }
            else return Ok(contaBancaria);

        }

        [HttpDelete]
        [Route("apagarcontabancaria/{id}")]
        [ProducesResponseType(typeof(ResponseModel<ContaBancariaResponse>), 200)]
        [ProducesResponseType(typeof(ResponseModel<ContaBancariaResponse>), 404)]
        [EndpointDescription("Método para apagar uma conta bancária com base no ID fornecido no Path.")]
        public async Task<ActionResult<ResponseModel<ContaBancariaResponse>>> ApagarContaBancaria(int id) {

            var contaBancaria = await _contaBancariaInterface.ApagarContaBancaria(id);

            if (contaBancaria.StatusCode == 404) {
                return NotFound(contaBancaria);
            }
            else return Ok(contaBancaria);
        }

    }
}
