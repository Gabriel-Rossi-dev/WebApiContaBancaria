using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebApiContaBancaria.Data;
using WebApiContaBancaria.Services.ContaBancaria;
using WebApiContaBancaria.Services.Transacoes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi(options => {
    options.AddDocumentTransformer((document, context, cancellationToken) => {

        document.Info = new() {
            Title = "Web Api Conta Bancaria ",
            Version = "1.0.0",
            Description = "Web API Conta Banc�ria � uma interface para contas banc�rias e transa��es." +
            " Projetada para facilitar a intera��o com sistemas banc�rios, essa API permite criar, atualizar e excluir contas," +
            " al�m de realizar dep�sitos, saques, transfer�ncias e consultar extratos." +
            " A interface tamb�m � integrada com a API da ReceitaWS para consultar CNPJ," +
            " possibilitando a valida��o e a obten��o de informa��es da empresa automaticamente."
        };

        document.Info.Contact = new() {
            Email = "costagabrielrd@gmail.com",
            Name = "Gabriel-Rossi-dev",
            Url = new Uri("https://github.com/Gabriel-Rossi-dev")
        };

        return Task.CompletedTask;

    });

});

builder.Services.AddScoped<IContaBancariaInterface, ContaBancariaService>();
builder.Services.AddScoped<ITransacoesInterface, TransacoesService>();

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment()) {
    app.MapScalarApiReference();

    app.MapOpenApi();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
