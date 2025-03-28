using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiContaBancaria.Models.Transacoes;
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
        public async Task<ActionResult<TransacaoResponse>> Deposito(DepositoRequest depositoRequest, int id) {

            var transacao = await _transacoesInterface.Deposito(depositoRequest, id);

            return Ok(transacao);
        }

        [HttpPost]
        [Route("saque{id}")]
        public async Task<ActionResult<TransacaoResponse>> Saque(SaqueRequest saqueRequest, int id) {

            var transacao = await _transacoesInterface.Saque(saqueRequest, id );

            return Ok(transacao);
        }

        [HttpPost]
        [Route("transferencia{id}")]
        public async Task<ActionResult<TransacaoResponse>> Tranferencia(TransferenciaRequest transacoesTransferenciaModelDto, int id) {

            var transacao = await _transacoesInterface.Transferencia(transacoesTransferenciaModelDto, id);

            return Ok(transacao);
        }

        [HttpGet]
        [Route("extrato{id}")]
        public async Task<ActionResult<ExtratoResponse>> Extrato(int id) {

            var transacoes = await _transacoesInterface.Extrato(id);
            return Ok(transacoes);

        }
    }
}
