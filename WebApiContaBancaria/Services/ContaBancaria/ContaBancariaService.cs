using Microsoft.EntityFrameworkCore;
using WebApiContaBancaria.Data;
using WebApiContaBancaria.Models.Response;
using WebApiContaBancaria.Request.ContaBancaria;
using WebApiContaBancaria.Response.ContaBancaria;
using WebApiContaBancaria.Converters.ContaBancaria;
using Newtonsoft.Json;

namespace WebApiContaBancaria.Services.ContaBancaria {
    public class ContaBancariaService : IContaBancariaInterface {

        private readonly AppDbContext _context;
        private static int contadorDeRequisicoes;
        private static DateTime? tempoPrimeiraRequisicao;
        private static readonly TimeSpan minuto = TimeSpan.FromSeconds(60);

        static ContaBancariaService() {
            contadorDeRequisicoes = 0;
            tempoPrimeiraRequisicao = DateTime.Now;
        }

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
                    resposta.Status = false;
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
                    resposta.Status = false;
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

            ResponseModel<ContaBancariaResponse> resposta = new ResponseModel<ContaBancariaResponse>();
            ContaCreateRequestToContaModel contaCreateRequestToContaModel = new ContaCreateRequestToContaModel();
            ContaBancariaModelToContaResponse contabancariaModelToContaResponse = new ContaBancariaModelToContaResponse();

            try {

                var conta = await _context.ContasBancarias.Where(contaBancariaModel => contaBancariaModel.Ativo).FirstOrDefaultAsync(contaBancariaModel =>
                    contaBancariaModel.Cnpj == contaBancariaCreateRequest.Cnpj &&
                    contaBancariaModel.Banco == contaBancariaCreateRequest.Banco);

                if (conta != null) {
                    resposta.Mensagem = "CNPJ já cadastrado para esse banco";
                    resposta.Status = false;
                    return resposta;
                }

                var numeroConta = await _context.ContasBancarias.Where(contaBancariaModel => contaBancariaModel.Ativo).FirstOrDefaultAsync(contaBancariaModel =>
                    contaBancariaModel.NumeroConta == contaBancariaCreateRequest.NumeroConta);

                if (numeroConta != null) {
                    resposta.Mensagem = "Ja existe esse numero de conta para outro CNPJ";
                    resposta.Status = false;
                    return resposta;
                }


                var request = new HttpRequestMessage(HttpMethod.Get, $"https://receitaws.com.br/v1/cnpj/{contaBancariaCreateRequest.Cnpj}");
                var client = new HttpClient();
                HttpResponseMessage response = null;

                if (contadorDeRequisicoes == 0) {
                    tempoPrimeiraRequisicao = DateTime.Now;
                    contadorDeRequisicoes++;
                    response = await client.SendAsync(request);
                }
                else if ((DateTime.Now - tempoPrimeiraRequisicao) > minuto) {
                    tempoPrimeiraRequisicao = DateTime.Now;
                    contadorDeRequisicoes = 0;
                    response = await client.SendAsync(request);
                }
                else if (contadorDeRequisicoes != 0 && (DateTime.Now - tempoPrimeiraRequisicao) < minuto) {
                    if (contadorDeRequisicoes == 3) {
                        TimeSpan tempoFaltante = minuto - (DateTime.Now - tempoPrimeiraRequisicao).Value;
                        tempoFaltante = TimeSpan.FromMilliseconds(Math.Ceiling(tempoFaltante.TotalMilliseconds));

                        if (tempoFaltante.TotalMilliseconds > 0) {
                            await Task.Delay(tempoFaltante);
                        }

                        response = await client.SendAsync(request);
                        contadorDeRequisicoes = 0;
                        tempoPrimeiraRequisicao = DateTime.Now;

                    }
                    else {
                        contadorDeRequisicoes++;
                        response = await client.SendAsync(request);
                    }
                }

                if (!response.IsSuccessStatusCode) {
                    resposta.Mensagem = "Erro ao consultar API externa";
                    resposta.Status = false;
                };

                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResult = JsonConvert.DeserializeObject<dynamic>(responseContent);

                if (apiResult.status == "ERROR") {
                    resposta.Mensagem = apiResult.message;
                    resposta.Status = false;
                    return resposta;
                }
                string nome = apiResult.nome;

                var contaBancariaModel = contaCreateRequestToContaModel.Convert(contaBancariaCreateRequest, nome);

                _context.Add(contaBancariaModel);
                await _context.SaveChangesAsync();

                var contas = await _context.ContasBancarias.Where(contas => contas.Ativo).FirstOrDefaultAsync(contas => contas.Cnpj == contaBancariaModel.Cnpj);

                if (contas == null) {
                    resposta.Mensagem = "Não foi possível salvar a conta.";
                    resposta.Status = false;
                    return resposta;
                }

                var contasResponse = contabancariaModelToContaResponse.Convert(contas);
                resposta.Dados = contasResponse;
                resposta.Mensagem = "Conta salva com sucesso!";
                return resposta;
            }
            catch (Exception ex) {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<ContaBancariaResponse>> AtualizarContaBancaria(ContaBancariaUpdateRequest contaBancariaUpdateRequest, int id) {

            ResponseModel<ContaBancariaResponse> resposta = new ResponseModel<ContaBancariaResponse>();
            ContaBancariaModelToContaResponse contabancariaModelToContaResponse = new ContaBancariaModelToContaResponse();


            try {

                var conta = await _context.ContasBancarias.Where(contas => contas.Ativo).FirstOrDefaultAsync(contas => contas.Id == id);

                if (conta == null) {
                    resposta.Mensagem = "Conta não encontrada!";
                    resposta.Status = false;
                    return resposta;

                }

                var numeroConta = await _context.ContasBancarias.Where(contaBancariaModel => contaBancariaModel.Ativo).FirstOrDefaultAsync(contaBancariaModel =>
                                        contaBancariaModel.NumeroConta == contaBancariaUpdateRequest.NumeroConta && contaBancariaModel.Id != id);

                if (numeroConta != null) {
                    resposta.Mensagem = "Ja existe esse numero de conta para outro CNPJ";
                    resposta.Status = false;
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
                    resposta.Status = false;
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

            try {

                var conta = await _context.ContasBancarias.Where(contas => contas.Ativo).FirstOrDefaultAsync(contas => contas.Id == id);

                if (conta == null) {
                    resposta.Mensagem = "Conta não encontrada!";
                    resposta.Status = false;
                    return resposta;
                }
                conta.Ativo = false;
                _context.Update(conta);
                await _context.SaveChangesAsync();

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
