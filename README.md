# Desafio Desenvolvedor Backend .NET 
# Web API Conta Bancária

## Introdução


Este documento tem como objetivo explicar em detalhes o desenvolvimento do desafio em .NET, abordando as tecnologias utilizadas, as funcionalidades implementadas e as regras de negócio aplicadas no desenvolvimento desta API. Através deste documento apresento uma visão abrangente do projeto, facilitando a compreensão do seu funcionamento e das decisões tomadas durante o processo de desenvolvimento.

# Tecnologias Utilizadas

- [.NET 9.03](https://dotnet.microsoft.com/pt-br/download)
- [Entity 9.0.3](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/9.0.3?_src=template)
- [Postgre Sql 9.0.4](https://www.nuget.org/packages/Npgsql.EntityFrameworkCore.PostgreSQL/9.0.4?_src=template)
- [Scalar 2.1.3](https://www.nuget.org/packages/Scalar.AspNetCore/2.1.3?_src=template) para documentação e testes interativos da API
- HttpClient para o consumo de APIs externas

## Instalação e Como Rodar

1. Clone o RepositórioPara obter o projeto localmente, execute o seguinte comando:
``` console
git clone https://github.com/Gabriel-Rossi-dev/WebApiContaBancaria.git
```
2. Abra o terminal na pasta do projeto clonado e execute o seguinte comando para criação da base de dados com suas tabelas:
``` console
  dotnet ef database update
```



## Orientações

- Recursos:
  - Conta bancária (CRUD).
  - Saque.
  - Depósito.
  - Transações (Uma conta para outra).
  - Retornar saldo e extrato.

- Sugestão de tabelas:
  - **Conta**: Campos: (id, nome, CNPJ, número da conta, agência e imagem do documento)
  - **Transações**: Campos: (id, valor, tipo, conta_id)

## Informações Adicionais

- Utilizar padrão REST, Postgres ou MySQL, e efetuar todas as validações necessárias.
- Ao realizar a abertura da conta, o nome da empresa não vai poder ser informado na model, deve ser obtido através da API pelo CNPJ informado. [ReceitaWS API](https://developers.receitaws.com.br/#/operations/queryCNPJFree) (Atenção ao limite, tem um nível gratuito, tratar erros).
- O documento da conta pode ser uma foto aleatória, fica a critério a forma de envio (Base64 ou MultipartFormData) salvar fisicamente em um diretório.

## O que será Avaliado

- Implementação dos recursos solicitados.
- Validações e tratamento de erros.
- Organização do código e estrutura do projeto.
- Uso adequado das tecnologias mencionadas (REST, banco de dados, .Net 5+).
- Clareza e qualidade do código.
- Uso de boas práticas de desenvolvimento.
- Documentação do projeto.

Qualquer dúvida pode ser enviada para o e-mail: marcos.rezende@inovamobil.com.br
