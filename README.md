Desafio ZxVentures Backend

## Como Executar a API

1) Acessar pelo console/prompt de comando a pasta do projeto ZxVentures.Backend.WebAPI e executar o comando "dotnet run".
1a) Caso ocorra um erro provavelmente será necessário instalar o .netcore (https://www.microsoft.com/net/core).
2) A aplicação irá iniciar.
3) Abrir seu navegador de preferência, entrar em http://localhost:5000/swagger que irá ter documentado todos os métodos da API.

## Rodando os testes

1) Acessar pelo console/prompt de comando a pasta do projeto ZxVentures.Backend.Tests
2) Executar "dotnet tests ZxVentures.Backend.Tests.csproj"

## Pequenas considerações acerca da API

- Os PDVs já estão sendo carregados no banco.
- A criação de um PDV não obecede ao Id que está sendo passado como parâmetro (o banco controla o seeding).
- Não foi pedido mas há possibilidade de excluir um PDV para poder testar a criação do mesmo.

## Libraries Utilizadas

## GeoJSON

Facilitar os calculos e criação de objetos de localização

https://github.com/GeoJSON-Net/GeoJSON.Net

## Swagger

Para facilitar a documentação da API

https://swagger.io/
