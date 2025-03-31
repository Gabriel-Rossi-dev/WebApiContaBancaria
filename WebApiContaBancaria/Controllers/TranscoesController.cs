using Microsoft.AspNetCore.Mvc;
using WebApiContaBancaria.Request.Transacoes;
using WebApiContaBancaria.Response.Transacoes;
using WebApiContaBancaria.Services.Transacoes;

namespace WebApiContaBancaria.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TranscoesController : ControllerBase {

        private readonly ITransacoesInterface _transacoesInterface;
        public TranscoesController(ITransacoesInterface transacoesInterface) {
            _transacoesInterface = transacoesInterface;
        }

        [HttpPost]
        [Route("deposito{id}")]
        [ProducesResponseType(typeof(TransacaoResponse), 201)]
        [ProducesResponseType(typeof(TransacaoResponse), 404)]
        [EndpointDescription("Método para depositar um valor na conta bancária especificada pelo ID no Path.")]
        public async Task<ActionResult<TransacaoResponse>> Deposito(DepositoRequest depositoRequest, int id) {

            var transacao = await _transacoesInterface.Deposito(depositoRequest, id);

            if (transacao.StatusCode == 404) {
                return NotFound(transacao);
            }
            else return Created($"transferencia/{id}", transacao);
        }

        [HttpPost]
        [Route("saque{id}")]
        [ProducesResponseType(typeof(TransacaoResponse), 201)]
        [ProducesResponseType(typeof(TransacaoResponse), 400)]
        [ProducesResponseType(typeof(TransacaoResponse), 404)]
        [EndpointDescription("Método para sacar um valor na conta bancária especificada pelo ID no Path.")]
        public async Task<ActionResult<TransacaoResponse>> Saque(SaqueRequest saqueRequest, int id) {

            var transacao = await _transacoesInterface.Saque(saqueRequest, id);

            if (transacao.StatusCode == 400) {
                return BadRequest(transacao);
            }
            else if (transacao.StatusCode == 404) {
                return NotFound(transacao);
            }
            else return Created($"transferencia/{id}", transacao);
        }

        [HttpPost]
        [Route("transferencia{id}")]
        [ProducesResponseType(typeof(TransacaoResponse), 201)]
        [ProducesResponseType(typeof(TransacaoResponse), 400)]
        [ProducesResponseType(typeof(TransacaoResponse), 404)]
        [EndpointDescription("Método para transferir um valor da conta bancária especificada pelo ID no Path para a conta bancária especificada no Body da requisição.")]
        public async Task<ActionResult<TransacaoResponse>> Tranferencia(TransferenciaRequest transacoesTransferenciaModelDto, int id) {

            var transacao = await _transacoesInterface.Transferencia(transacoesTransferenciaModelDto, id);


            if (transacao.StatusCode == 400) {
                return BadRequest(transacao);
            }
            else if (transacao.StatusCode == 404) {
                return NotFound(transacao);
            }
            else return Created($"transferencia/{id}", transacao);
        }

        [HttpGet]
        [Route("extrato{id}")]
        [ProducesResponseType(typeof(ExtratoResponse), 200)]
        [ProducesResponseType(typeof(ExtratoResponse), 404)]
        [EndpointDescription("Método para recuperar o saldo e um extrato de todas as movimentações da conta bancária especificada pelo ID no Path.")]
        public async Task<ActionResult<ExtratoResponse>> Extrato(int id) {

            var transacoes = await _transacoesInterface.Extrato(id);

            if (transacoes.StatusCode == 404) {
                return NotFound(transacoes);
            }
            else return Ok(transacoes);
        }

    }

}
