using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiContaBancaria.Data;
using WebApiContaBancaria.Models.ContaBancariaModel;
using WebApiContaBancaria.Models.Response;
using WebApiContaBancaria.Request.ContaBancaria;
using WebApiContaBancaria.Response.ContaBancaria;
using WebApiContaBancaria.Converters.ContaBancaria;
using static System.Net.Mime.MediaTypeNames;

namespace WebApiContaBancaria.Services.ContaBancaria {
    public class ContaBancariaService : IContaBancariaInterface {

        private readonly AppDbContext _context;

        public ContaBancariaService(AppDbContext context) {
            _context = context;
        }
        public async Task<ResponseModel<List<ContaBancariaResponse>>> GetContasBancarias() {

            ResponseModel<List<ContaBancariaResponse>> resposta = new ResponseModel<List<ContaBancariaResponse>>();
            ContaBancariaModelToContaResponse contabancariaModelToContaResponse = new ContaBancariaModelToContaResponse();

            try {

                var contas = await _context.ContasBancarias.Where(contasBancarias => contasBancarias.Ativo).ToListAsync();
                if (contas == null) {

                    resposta.Mensagem = "Nenhuma conta Localizada!";
                    return resposta;
                }

                var contasResponse = contas.Select(conta => contabancariaModelToContaResponse.Convert(conta)).ToList();

                resposta.Dados = contasResponse;
                resposta.Mensagem = "Todos as Contas foram coletadas!";
                return resposta;
            }
            catch (Exception ex) {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            };

        }

        public async Task<ResponseModel<ContaBancariaResponse>> GetContaPorId(int idContaBancaria) {

            ResponseModel<ContaBancariaResponse> resposta = new ResponseModel<ContaBancariaResponse>();
            ContaBancariaModelToContaResponse contabancariaModelToContaResponse = new ContaBancariaModelToContaResponse();



            try {

                var conta = await _context.ContasBancarias.Where(contaBancariaModel =>
                contaBancariaModel.Ativo).FirstOrDefaultAsync(contaBancariaModel =>
                contaBancariaModel.Id == idContaBancaria);

                if (conta == null) {
                    resposta.Mensagem = "Nenhuma conta Localizada!";
                    return resposta;
                }

                var contaResponse = contabancariaModelToContaResponse.Convert(conta);

                resposta.Mensagem = "Conta Localizada!";
                resposta.Dados = contaResponse;
                return resposta;


            }
            catch (Exception ex) {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            };
        }

        public async Task<ResponseModel<ContaBancariaResponse>> CriarContaBancaria(ContaBancariaCreateRequest contaBancariaCreateRequest) {


            //aqui 
            //preciso consumir a api https://developers.receitaws.com.br/#/operations/queryCNPJFree
            //Para consumir a API é utilizado um CNPJ Válido sem caracteres especiais: exemplo 44609664000209
            //precisa ser feito a tratativa de conseguir dar 3 requests por minuto.
            // as informações dessa api serão passadas diretamente para a ContaBancariaModel
            // vou precisar das informações de Nome, CNPJ
            // numeroConta, Banco e ImageBase64 o front envia.

            //O CNPJ será um registro UNICO, não podendo ser repetido.


            ResponseModel<ContaBancariaResponse> resposta = new ResponseModel<ContaBancariaResponse>();
            ContaCreateRequestToContaModel contaCreateRequestToContaModel = new ContaCreateRequestToContaModel();
            ContaBancariaModelToContaResponse contabancariaModelToContaResponse = new ContaBancariaModelToContaResponse();

            try {

                //aqui
                //precisa melhorar essa logica.
                var conta = await _context.ContasBancarias.Where(contaBancariaModel => contaBancariaModel.Ativo).FirstOrDefaultAsync(contaBancariaModel =>
                    contaBancariaModel.Cnpj == contaBancariaCreateRequest.Cnpj &&
                    contaBancariaModel.Banco == contaBancariaCreateRequest.Banco);

                //aqui
                //precisa melhorar essa mensagem.
                if (conta != null) {
                    resposta.Mensagem = "CNPJ já cadastrado para esse banco";
                    return resposta;
                }

                var contaBancariaModel = contaCreateRequestToContaModel.Convert(contaBancariaCreateRequest);


                _context.Add(contaBancariaModel);
                await _context.SaveChangesAsync();

                var contas = await _context.ContasBancarias.Where(contas => contas.Ativo).ToListAsync();

                var contasResponse = contabancariaModelToContaResponse.Convert(conta);


                resposta.Dados = contasResponse;
                resposta.Mensagem = "Conta salva com sucesso!";
                return resposta;

            }
            catch (Exception ex) {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            };
        }

        public async Task<ResponseModel<ContaBancariaResponse>> AtualizarContaBancaria(ContaBancariaUpdateRequest contaBancariaUpdateRequest, int id) {

            ResponseModel<ContaBancariaResponse> resposta = new ResponseModel<ContaBancariaResponse>();
            ContaBancariaModelToContaResponse contabancariaModelToContaResponse = new ContaBancariaModelToContaResponse();


            try {

                var conta = await _context.ContasBancarias.Where(contas => contas.Ativo).FirstOrDefaultAsync(contas => contas.Id == id);

                if (conta == null) {
                    resposta.Mensagem = "Conta não encontrada!";
                    return resposta;

                }

                conta.NumeroConta = contaBancariaUpdateRequest.NumeroConta;
                conta.Agencia = contaBancariaUpdateRequest.Agencia;
                conta.Banco = contaBancariaUpdateRequest.Banco;
                conta.Ativo = true;

                _context.Update(conta);
                await _context.SaveChangesAsync();

                var contaAtualizada = await _context.ContasBancarias.Where(contaBancaria => contaBancaria.Ativo).FirstOrDefaultAsync(contaBancariaModel =>
                    contaBancariaModel.Id == id);

                if (contaAtualizada == null) {
                    resposta.Mensagem = "Não foi possível atulizar as informações.";
                    return resposta;
                }

                var contaResponse = contabancariaModelToContaResponse.Convert(contaAtualizada);

                resposta.Dados = contaResponse;
                resposta.Mensagem = "Conta atualizada com sucesso!";
                return resposta;

            }
            catch (Exception ex) {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<ContaBancariaResponse>> ApagarContaBancaria(int id) {


            ResponseModel<ContaBancariaResponse> resposta = new ResponseModel<ContaBancariaResponse>();
            ContaBancariaModelToContaResponse contabancariaModelToContaResponse = new ContaBancariaModelToContaResponse();


            try {

                var conta = await _context.ContasBancarias.Where(contas => contas.Ativo).FirstOrDefaultAsync(contas => contas.Id == id);

                if (conta == null) {
                    resposta.Mensagem = "Conta não encontrada!";
                    return resposta;
                }
                conta.Ativo = false;
                _context.Update(conta);
                await _context.SaveChangesAsync();


                var contasBancarias = await _context.ContasBancarias.Where(contas => contas.Ativo).ToListAsync();

                var contasResponse = contabancariaModelToContaResponse.Convert(conta);

                resposta.Dados = contasResponse;
                resposta.Mensagem = "Conta apagada com sucesso!";
                return resposta;

            }
            catch (Exception ex) {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            };

        }


    }
}
