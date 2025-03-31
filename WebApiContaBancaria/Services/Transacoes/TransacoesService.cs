using Microsoft.EntityFrameworkCore;
using WebApiContaBancaria.Converters.Transacao;
using WebApiContaBancaria.Data;
using WebApiContaBancaria.Models.Response;
using WebApiContaBancaria.Models.Transacoes;
using WebApiContaBancaria.Request.Transacoes;
using WebApiContaBancaria.Response.Transacoes;

namespace WebApiContaBancaria.Services.Transacoes {
    public class TransacoesService : ITransacoesInterface {

        private readonly AppDbContext _context;

        public TransacoesService(AppDbContext context) {
            _context = context;
        }

        public async Task<ResponseModel<TransacaoResponse>> Deposito(DepositoRequest depositoRequest, int id) {
            ResponseModel<TransacaoResponse> resposta = new ResponseModel<TransacaoResponse>();
            TransacaoModelToTransacaoResponse transacaoModelToTransacaoResponse = new TransacaoModelToTransacaoResponse();


            try {
                var contaBancaria = await _context.ContasBancarias.Where(conta => conta.Ativo).FirstOrDefaultAsync(conta => conta.Id == id);
                if (contaBancaria == null) {
                    resposta.Mensagem = "A conta informada não existe!";
                    resposta.Status = false;
                    return resposta;
                }

                TransacoesModel transacoesModel = new TransacoesModel(0, id, depositoRequest.Valor, "Deposito", DateTime.UtcNow);
                _context.Add(transacoesModel);
                await _context.SaveChangesAsync();

                var transacaoResponse = transacaoModelToTransacaoResponse.Convert(transacoesModel);

                resposta.Dados = transacaoResponse;
                resposta.Mensagem = "Depósito efetuado com sucesso!";
                return resposta;
            }
            catch (Exception ex) {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<TransacaoResponse>> Saque(SaqueRequest saqueRequest, int id) {
            ResponseModel<TransacaoResponse> resposta = new ResponseModel<TransacaoResponse>();
            TransacaoModelToTransacaoResponse transacaoModelToTransacaoResponse = new TransacaoModelToTransacaoResponse();


            try {
                var contaBancaria = await _context.ContasBancarias.Where(conta => conta.Ativo).FirstOrDefaultAsync(conta => conta.Id == id);
                if (contaBancaria == null) {
                    resposta.Mensagem = "A conta informada não existe!";
                    return resposta;
                }

                decimal saldo = await Saldo(id);
                if (saldo < saqueRequest.Valor) {
                    resposta.Mensagem = $"Sem saldo suficiente. O valor máximo de saque é: {saldo}";
                    return resposta;
                }

                TransacoesModel transacoesModel = new TransacoesModel(id, 0, saqueRequest.Valor, "Saque", DateTime.UtcNow);
                _context.Add(transacoesModel);
                await _context.SaveChangesAsync();

                var transacaoResponse = transacaoModelToTransacaoResponse.Convert(transacoesModel);

                resposta.Dados = transacaoResponse;
                resposta.Mensagem = "Saque efetuado com sucesso!";
                return resposta;
            }
            catch (Exception ex) {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }

        }

        public async Task<ResponseModel<TransacaoResponse>> Transferencia(TransferenciaRequest transacoesTransferenciaModelDto, int id) {
            ResponseModel<TransacaoResponse> resposta = new ResponseModel<TransacaoResponse>();
            TransacaoModelToTransacaoResponse transacaoModelToTransacaoResponse = new TransacaoModelToTransacaoResponse();


            try {
                var contaOrigem = await _context.ContasBancarias.Where(conta => conta.Ativo).FirstOrDefaultAsync(conta => conta.Id == id);
                if (contaOrigem == null) {
                    resposta.Mensagem = "A conta de Origem não existe!";
                    return resposta;
                }

                var contaDestino = await _context.ContasBancarias.Where(conta => conta.Ativo).FirstOrDefaultAsync(conta => conta.Id == transacoesTransferenciaModelDto.IdContaDestino);
                if (contaDestino == null) {
                    resposta.Mensagem = "A conta de Destino não existe!";
                    return resposta;
                }

                decimal saldoOrigem = await Saldo(id);
                if (saldoOrigem < transacoesTransferenciaModelDto.Valor) {
                    resposta.Mensagem = $"Sem saldo suficiente. O valor máximo para transferencia é: {saldoOrigem}";
                    return resposta;
                }

                TransacoesModel transacoesModel = new TransacoesModel(id, transacoesTransferenciaModelDto.IdContaDestino, transacoesTransferenciaModelDto.Valor, "Transferencia", DateTime.UtcNow);
                _context.Add(transacoesModel);
                await _context.SaveChangesAsync();

                var transacaoResponse = transacaoModelToTransacaoResponse.Convert(transacoesModel);

                resposta.Dados = transacaoResponse;
                resposta.Mensagem = "Transferencia efetuada com sucesso!";
                return resposta;
            }
            catch (Exception ex) {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }

        }

        public async Task<ResponseModel<ExtratoResponse>> Extrato(int id) {

            List<TransacoesModel> transacoesModel = new List<TransacoesModel>();
            ResponseModel<ExtratoResponse> resposta = new ResponseModel<ExtratoResponse> {
                Dados = new ExtratoResponse()
            };


            try {

                var contaBancaria = await _context.ContasBancarias.Where(conta => conta.Ativo).FirstOrDefaultAsync(conta => conta.Id == id);
                if (contaBancaria == null) {
                    resposta.Mensagem = "A conta informada não existe!";
                    return resposta;
                }

                transacoesModel = await _context.Transacoes.Where(conta => conta.IdContaDestino == id || conta.IdContaOrigem == id).OrderBy(data => data.Data).ToListAsync();
                if (transacoesModel.Count == 0) {
                    resposta.Mensagem = "Não existem movimentações para essa conta";
                    resposta.Dados.Extrato = null;
                    return resposta;
                }

                resposta.Dados.Extrato = transacoesModel;
                decimal saldo = await Saldo(id);
                resposta.Dados.Saldo = saldo;
                resposta.Mensagem = "Extrato retornado com Sucesso";

                return resposta;


            }
            catch (Exception ex) {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            };

        }

        private async Task<decimal> Saldo(int id) {

            var entradas = await _context.Transacoes.Where(idDestino => idDestino.IdContaDestino == id).SumAsync(t => t.Valor);
            var saidas = await _context.Transacoes.Where(idOrigem => idOrigem.IdContaOrigem == id).SumAsync(t => t.Valor);
            var saldo = entradas - saidas;
            return saldo;

        }
    }
}

