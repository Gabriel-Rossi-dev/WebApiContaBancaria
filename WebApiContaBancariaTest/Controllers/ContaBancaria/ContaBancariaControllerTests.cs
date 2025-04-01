using Moq;
using Microsoft.AspNetCore.Mvc;
using WebApiContaBancaria.Controllers;
using WebApiContaBancaria.Services.ContaBancaria;
using WebApiContaBancaria.Models.Response;
using WebApiContaBancaria.Request.ContaBancaria;
using WebApiContaBancaria.Response.ContaBancaria;


public class ContaBancariaControllerTests {

    private readonly Mock<IContaBancariaInterface> _mockService;
    private readonly ContaBancariaController _controller;
    private readonly ContaBancariaResponse _response;

    public ContaBancariaControllerTests() {
        _mockService = new Mock<IContaBancariaInterface>();
        _controller = new ContaBancariaController(_mockService.Object);
        _response = BuildContaBancariaResponse();
    }

    [Fact]
    public async Task GetContasBancarias_ReturnsOkResult() {

        var response = new ResponseModel<List<ContaBancariaResponse>> { StatusCode = 200, Dados = new List<ContaBancariaResponse> { _response } };
        _mockService.Setup(service => service.GetContasBancarias()).ReturnsAsync(response);

        var result = await _controller.GetContasBancarias();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, okResult.StatusCode);
        var responseModel = Assert.IsType<ResponseModel<List<ContaBancariaResponse>>>(okResult.Value);
        Assert.NotNull(responseModel.Dados);
        Assert.NotEmpty(responseModel.Dados);
    }

    [Fact]
    public async Task GetContasBancarias_ReturnsNotFoundResult() {

        var response = new ResponseModel<List<ContaBancariaResponse>> { StatusCode = 404, Dados = null };
        _mockService.Setup(service => service.GetContasBancarias()).ReturnsAsync(response);

        var result = await _controller.GetContasBancarias();

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task GetContaPorId_ReturnsNotFoundResult() {

        var response = new ResponseModel<ContaBancariaResponse> { StatusCode = 404, Dados = null };
        _mockService.Setup(service => service.GetContaPorId(It.IsAny<int>())).ReturnsAsync(response);

        var result = await _controller.GetContaPorId(1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task GetContaPorId_ReturnsOkResult() {

        var response = new ResponseModel<ContaBancariaResponse> { StatusCode = 200, Dados = _response };
        _mockService.Setup(service => service.GetContaPorId(It.IsAny<int>())).ReturnsAsync(response);

        var result = await _controller.GetContaPorId(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, okResult.StatusCode);
        var responseModel = Assert.IsType<ResponseModel<ContaBancariaResponse>>(okResult.Value);
        Assert.NotNull(responseModel.Dados);
        Assert.Equal(_response, response.Dados);
    }

    [Fact]
    public async Task CriarContaBancaria_ReturnsCreatedResult() {

        var request = new ContaBancariaCreateRequest();
        var response = new ResponseModel<ContaBancariaResponse> { StatusCode = 201, Dados = _response };
        _mockService.Setup(service => service.CriarContaBancaria(request)).ReturnsAsync(response);

        var result = await _controller.CriarContaBancaria(request);

        var createdResult = Assert.IsType<CreatedResult>(result.Result);
        Assert.Equal(201, createdResult.StatusCode);
    }

    [Fact]
    public async Task CriarContaBancaria_ReturnsBadRequestResult() {

        var request = new ContaBancariaCreateRequest();
        var response = new ResponseModel<ContaBancariaResponse> { StatusCode = 400, Dados = _response };
        _mockService.Setup(service => service.CriarContaBancaria(request)).ReturnsAsync(response);

        var result = await _controller.CriarContaBancaria(request);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task CriarContaBancaria_ReturnsNotFoundResult() {

        var request = new ContaBancariaCreateRequest();
        var response = new ResponseModel<ContaBancariaResponse> { StatusCode = 404, Dados = _response };
        _mockService.Setup(service => service.CriarContaBancaria(request)).ReturnsAsync(response);

        var result = await _controller.CriarContaBancaria(request);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task AtualizarContaBancaria_ReturnsBadRequestResult() {

        var request = new ContaBancariaUpdateRequest();
        var response = new ResponseModel<ContaBancariaResponse> { StatusCode = 400 };
        _mockService.Setup(service => service.AtualizarContaBancaria(request, It.IsAny<int>())).ReturnsAsync(response);

        var result = await _controller.AtualizarContaBancaria(request, 1);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task AtualizarContaBancaria_ReturnsNotFoundResult() {

        var request = new ContaBancariaUpdateRequest();
        var response = new ResponseModel<ContaBancariaResponse> { StatusCode = 404 };
        _mockService.Setup(service => service.AtualizarContaBancaria(request, It.IsAny<int>())).ReturnsAsync(response);

        var result = await _controller.AtualizarContaBancaria(request, 1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task AtualizarContaBancaria_ReturnsCreatedResult() {

        var request = new ContaBancariaUpdateRequest();
        var response = new ResponseModel<ContaBancariaResponse> { StatusCode = 400 };
        _mockService.Setup(service => service.AtualizarContaBancaria(request, It.IsAny<int>())).ReturnsAsync(response);

        var result = await _controller.AtualizarContaBancaria(request, 1);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task ApagarContaBancaria_ReturnsOkResult() {

        var response = new ResponseModel<ContaBancariaResponse> { StatusCode = 200 };
        _mockService.Setup(service => service.ApagarContaBancaria(It.IsAny<int>())).ReturnsAsync(response);

        var result = await _controller.ApagarContaBancaria(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task ApagarContaBancaria_ReturnsNotFoundResult() {

        var response = new ResponseModel<ContaBancariaResponse> { StatusCode = 404 };
        _mockService.Setup(service => service.ApagarContaBancaria(It.IsAny<int>())).ReturnsAsync(response);

        var result = await _controller.ApagarContaBancaria(1);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }

    private ContaBancariaResponse BuildContaBancariaResponse() {

        return new ContaBancariaResponse(
            id: 1,
            nome: "Conta Bancaria",
            cnpj: "13590585000270",
            numeroConta: "45612347",
            agencia: "9816"
        );
    }
}
