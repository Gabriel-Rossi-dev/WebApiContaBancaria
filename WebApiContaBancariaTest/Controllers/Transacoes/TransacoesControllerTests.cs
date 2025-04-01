using Moq;
using Microsoft.AspNetCore.Mvc;
using WebApiContaBancaria.Controllers;
using WebApiContaBancaria.Services.Transacoes;
using WebApiContaBancaria.Models.Response;
using WebApiContaBancaria.Request.Transacoes;
using WebApiContaBancaria.Response.Transacoes;
using WebApiContaBancaria.Models.Transacoes;

public class TranscoesControllerTests {

    private readonly Mock<ITransacoesInterface> _mockService;
    private readonly TranscoesController _controller;
    private readonly TransacaoResponse _transacaoResponse;
    private readonly ExtratoResponse _extratoResponse;

    public TranscoesControllerTests() {
        _mockService = new Mock<ITransacoesInterface>();
        _controller = new TranscoesController(_mockService.Object);
        _transacaoResponse = BuildTransacaoResponse();
        _extratoResponse = BuildExtratoResponse();
    }

    [Fact]
    public async Task Deposito_ReturnsCreatedResult() {
        
        var request = new DepositoRequest { Valor = 100 };
        var response = new ResponseModel<TransacaoResponse> { StatusCode = 201, Dados = _transacaoResponse };
        _mockService.Setup(service => service.Deposito(request, It.IsAny<int>())).ReturnsAsync(response);

        var result = await _controller.Deposito(request, 1);

        var createdResult = Assert.IsType<CreatedResult>(result.Result);
        Assert.Equal(201, createdResult.StatusCode);
    }

    [Fact]
    public async Task Deposito_ReturnsNotFoundResult() {

        var request = new DepositoRequest { Valor = 100 };
        var response = new ResponseModel<TransacaoResponse> { StatusCode = 404, Dados = null };
        _mockService.Setup(service => service.Deposito(request, It.IsAny<int>())).ReturnsAsync(response);

        var result = await _controller.Deposito(request, 1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task Saque_ReturnsCreatedResult() {

        var request = new SaqueRequest { Valor = 100 };
        var response = new ResponseModel<TransacaoResponse> { StatusCode = 201, Dados = _transacaoResponse };
        _mockService.Setup(service => service.Saque(request, It.IsAny<int>())).ReturnsAsync(response);

        var result = await _controller.Saque(request, 1);

        var createdResult = Assert.IsType<CreatedResult>(result.Result);
        Assert.Equal(201, createdResult.StatusCode);
    }

    [Fact]
    public async Task Saque_ReturnsBadRequestResult() {

        var request = new SaqueRequest { Valor = 100 };
        var response = new ResponseModel<TransacaoResponse> { StatusCode = 400, Dados = null };
        _mockService.Setup(service => service.Saque(request, It.IsAny<int>())).ReturnsAsync(response);

        var result = await _controller.Saque(request, 1);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task Saque_ReturnsNotFoundResult() {

        var request = new SaqueRequest { Valor = 100 };
        var response = new ResponseModel<TransacaoResponse> { StatusCode = 404, Dados = null };
        _mockService.Setup(service => service.Saque(request, It.IsAny<int>())).ReturnsAsync(response);

        var result = await _controller.Saque(request, 1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task Transferencia_ReturnsCreatedResult() {

        var request = new TransferenciaRequest { IdContaDestino = 2, Valor = 100 };
        var response = new ResponseModel<TransacaoResponse> { StatusCode = 201, Dados = _transacaoResponse };
        _mockService.Setup(service => service.Transferencia(request, It.IsAny<int>())).ReturnsAsync(response);

        var result = await _controller.Tranferencia(request, 1);

        var createdResult = Assert.IsType<CreatedResult>(result.Result);
        Assert.Equal(201, createdResult.StatusCode);
    }

    [Fact]
    public async Task Transferencia_ReturnsBadRequestResult() {

        var request = new TransferenciaRequest { IdContaDestino = 2, Valor = 100 };
        var response = new ResponseModel<TransacaoResponse> { StatusCode = 400, Dados = null };
        _mockService.Setup(service => service.Transferencia(request, It.IsAny<int>())).ReturnsAsync(response);

        var result = await _controller.Tranferencia(request, 1);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task Transferencia_ReturnsNotFoundResult() {

        var request = new TransferenciaRequest { IdContaDestino = 2, Valor = 100 };
        var response = new ResponseModel<TransacaoResponse> { StatusCode = 404, Dados = null };
        _mockService.Setup(service => service.Transferencia(request, It.IsAny<int>())).ReturnsAsync(response);

        var result = await _controller.Tranferencia(request, 1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task Extrato_ReturnsOkResult() {

        var response = new ResponseModel<ExtratoResponse> { StatusCode = 200, Dados = _extratoResponse };
        _mockService.Setup(service => service.Extrato(It.IsAny<int>())).ReturnsAsync(response);

        var result = await _controller.Extrato(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, okResult.StatusCode);
        var responseModel = Assert.IsType<ResponseModel<ExtratoResponse>>(okResult.Value);
        Assert.NotNull(responseModel.Dados);
    }

    [Fact]
    public async Task Extrato_ReturnsNotFoundResult() {

        var response = new ResponseModel<ExtratoResponse> { StatusCode = 404, Dados = null };
        _mockService.Setup(service => service.Extrato(It.IsAny<int>())).ReturnsAsync(response);

        var result = await _controller.Extrato(1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    private TransacaoResponse BuildTransacaoResponse() {
        return new TransacaoResponse(
            idContaOrigem: 1,
            idContaDestino: 2,
            valor: 100,
            tipo: "Transferencia",
            data: DateTime.Now
        );
    }

    private ExtratoResponse BuildExtratoResponse() {
        return new ExtratoResponse {
            Extrato = new List<TransacoesModel> {
                new TransacoesModel(1, 2, 100, "Transferencia", DateTime.Now),
                new TransacoesModel(1, 2, 500, "Saque", DateTime.Now),
                new TransacoesModel(1, 2, 1500, "Deposito", DateTime.Now)
            },
            Saldo = 900
        };
    }
}
