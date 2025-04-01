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

1. Clone o Repositório para obter o projeto localmente, execute o seguinte comando:
    ``` console
    git clone https://github.com/Gabriel-Rossi-dev/WebApiContaBancaria.git
    ```

2. Instale o Dotnet 9.0.3 no link abaixo.
    - https://dotnet.microsoft.com/pt-br/download


3. Instale os pacotes necessários para o funcionamento da api executando os seguintes comandos em ordem:
    Abra o terminal na pasta do projeto clonado e execute os comandos abaixo. 
  
    - ``` console
        dotnet add package Microsoft.EntityFrameworkCore --version 9.0.3
      ```
  
    - ``` console 
        dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 9.0.4
      ``` 
      
    - ``` console 
        dotnet add package Scalar.AspNetCore --version 2.1.3
      ``` 


4. Instale o Banco de Dados PostgreSql na sua maquina na versão 17.4, link abaixo:
    - Antes de instalar é preciso ficar atento ao Username e ao Password, porque eles serão necessários para configurar o arquivo de conexão com o banco.
    - [PostgreSql 17.4 WINDOWNS](https://sbp.enterprisedb.com/getfile.jsp?fileid=1259402)
      

5. Abra o arquivo appSettings.json e altere as informações de DefaultConnetion de acordo com as credencias do seu PostegreSQL instalado.
 
   - Se necessário altere o Host.
   - Altere o Username de acordo com o username do seu postgre informado no ato de instalação.
   - Altere o Password de acordo com o password do seu postgre informado no ato de instalação.
     
   Exemplo de connectionStrings:
     - Host=localhost;Username=postgres;Password=123;Database=WebApiContaBancaria
       
   Onde está 'Username=postgres' você irá trocar o 'postgres' para o Username que foi definido na instalação do banco de dados Postgre.
   Onde está 'Password=123' você irá trocar o '123' para o Password que foi definido na instalação do banco de dados Postgre.

6. Execute o seguinte comando para criação da base de dados e suas tabelas:
``` console
  dotnet ef database update
```


# Estrutura do Projeto

O projeto está estruturado nas seguintes camadas, cada uma representada por uma pasta:

## Controllers

Os controladores lidam com as requisições HTTP recebidas e retornam respostas ao cliente. Eles interagem com a camada de serviços para executar a lógica de negócios.

### Operações CRUD para contas bancárias:
- **GET** `/api/contas` → Obtém todas as contas bancárias.
- **GET** `/api/contas/{id}` → Obtém uma conta bancária específica pelo ID.
- **POST** `/api/criarcontabancaria` → Cria uma nova conta bancária.
- **PUT** `/api/atualizarcontabancaria/{id}` → Atualiza uma conta bancária existente.
- **DELETE** `/api/apagarcontabancaria/{id}` → Remove uma conta bancária.

### Operações para Transações:
- **POST** `/api/Transacoes/deposito/{id}` → Cria uma nova conta transação do tipo 'Depósito' específica pelo ID.
- **POST** `/api/Transacoes/saque/{id}` → Cria uma nova conta transação do tipo 'Saque' específica pelo ID.
- **POST** `/api/Transacoes/transferencia/{id}` → Cria uma nova conta transação do tipo 'Transferencia'. Onde retira o dinheiro da Conta Origem para a Conta Destino.
- **GET** `/api/Transacoes/extrato/{id}` → Obtém o saldo e todas movimentações da conta bancária específica pelo ID.

## Models

Os modelos são as estruturas de dados modelo utilizadas na aplicação.
- Modelo de cada tabela que é salvo no banco de dados.
- Modelo genérico de resposta usado para padronizar as respostas da API.

## Request

As classes de Request(requisição) definem o formato dos dados enviados pelo cliente para a API.

Cada Request tem sua funcionalidade Única sendo todos os tipos de entradas de dados provenientes do cliente.
Sendo elas: 
- ContaBancariaCreateRequest representa os dados de uma conta bancária enviados pelo cliente através da API para a criação de uma conta Bancária.
- ContaBancariaUpdateRequest representa os dados de uma conta bancária enviados pelo cliente através da API para atualização de uma conta Bancária.
- DepositoRequest representa os dados de uma transação enviados pelo cliente através da API para criação de uma transação do tipo Depósito.
- SaqueRequest representa os dados de uma transação enviados pelo cliente através da API para criação de uma transação do tipo Saque.
- TransferenciaRequest representa os dados de uma transação enviados pelo cliente através da API para criação de uma transação do tipo Transferencia.

## Response

As classes de Response(resposta) definem a estrutura dos dados retornados pela API ao cliente.

- ContaBancariaResponse representa os dados de uma conta bancária retornados pela API.
- TransacaoResponse representa os dados de uma transação retornada pela API.
- ExtratoResponse representa os dados de extrato e saldo de uma Conta Bancaria retornada pela API.

## Services

Os Services (serviços) contêm a lógica de negócios da aplicação. Eles se comunicam com a camada de dados para executar operações.
- ContaBancariaService implementa os métodos da **IContaBancariaInterface** para gerenciar contas bancárias.
- TransacoesService Implementa os métodos da **ITransacoesInterface** para lidar com operações de transações.
  
## Converters

Os Converters(conversores) lidam com a transformação de dados entre diferentes camadas, como converter dados de entrada para modelos de banco de dados e modelos de banco de dados para dados de saída.
Os nomes são bem intuitivos

- **TransacaoModelToTransacaoResponse.cs** → Converte **TransacoesModel** para **TransacaoResponse**.
- **ContaCreateRequestToContaModel.cs** → Converte **ContaCreateRequest** para **ContaModel**.
- **TransacaoModelToTransacaoResponse.cs** → Converte **TransacoesModel** para **TransacaoResponse**.

## Utils

A pasta **Utils** contém lógica de validação para os dados recebidos do cliente, garantindo a integridade dos dados e prevenindo erros.
Cada Modelo de Request(requisição) tem suas validações através de Data Notations em cada propriedade dessa classe.

- A classe **AgenciaValidation** → Valida o valor enviado do dado agencia que só permitirá a entrada caso atenda a critérios específicos.
- A classe **Base64Validation** → Valida o valor enviado do dado documento que só permitirá a entrada caso atenda a critérios específicos.
- A classe **CnpjValidation** → Valida o valor enviado do dado CNPJ que só permitirá a entrada caso atenda a critérios específicos.
- A classe **NumeroContaValidation** → Valida o valor enviado do dado numeroConta que só permitirá a entrada caso atenda a critérios específicos.
- A classe **ValorValidation** → Valida o valor enviado do dado valor que só permitirá a entrada caso atenda a critérios específicos.

## Data

A camada de dados gerencia as interações com o banco de dados.
- Configura o mapeamento de entidades e o contexto do banco de dados.
