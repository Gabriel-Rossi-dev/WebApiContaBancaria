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
