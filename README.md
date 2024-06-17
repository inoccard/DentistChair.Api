# Sistema de Cadastro de Cadeiras de Dentista
## Descrição
Este projeto é uma API para gerenciamento de cadeiras de dentista utilizando .NET 8, seguindo os princípios de DDD (Domain-Driven Design), Clean Code e SOLID. Utiliza EF Core para acesso ao banco de dados MySQL, FluentValidation para validação, AutoMapper para mapeamento de objetos e Swagger para documentação da API.

## Estrutura do Projeto
- Api: Contém os controladores e configurações da API.
- Data: Contém o contexto do banco de dados e configurações do EF Core.
- Domain: Contém as entidades e interfaces de repositório.
- Validation: Contém as validações dos modelos utilizando FluentValidation.

## Tecnologias Utilizadas
- .NET 8
- DDD (Domain-Driven Design)
- Clean Code
- SOLID
- EF Core
- FluentValidation
- Swagger
- AutoMapper
- Migrations

## Pacotes Utilizados
- AutoMapper
- FluentValidation
- Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
- Microsoft.EntityFrameworkCore.Design
- Microsoft.VisualStudio.Azure.Containers.Tools.Targets
- Pomelo.EntityFrameworkCore.MySql
- Swashbuckle.AspNetCore
- Microsoft.EntityFrameworkCore.Relational

## Requisitos Funcionais
- O sistema deve permitir o cadastro de novas cadeiras de dentista, incluindo informações como número da cadeira, descrição, e outra informação relevante.
- Deve ser possível visualizar todas as cadeiras cadastradas.
- O usuário deve poder atualizar as informações de uma cadeira existente.
- O sistema deve permitir a exclusão de cadeiras.
- Implemente um método de alocação automática de cadeiras informando data e hora inicial e final, rotacionando as cadeiras utilizadas de forma a distribuir proporcionalmente as alocações entre elas de acordo com a quantidade de horários selecionados.

## Requisitos Técnicos
- Utilize a plataforma .NET para desenvolver a API.
- Utilize uma base de dados MySQL para armazenar as informações.
- Utilize boas práticas de desenvolvimento, incluindo padrões de projeto, separação de responsabilidades e validação de dados.

  ## Configuração e Execução
### Pré-requisitos
- .NET 8 SDK
- MySQL

## Configuração do Banco de Dados
Configure a string de conexão para o banco de dados MySQL no arquivo appsettings.json da camada Api:
```{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=DentalChairsDb;User=root;Password=yourpassword;"
  }
}
```
## Executar Migrations
Basta rodar o projeto e todas as migrations serão executadas de forma automática

## Endpoints da API
### Allocation
- POST /api/allocation/allocate - Aloca cadeiras automaticamente.

### Chair
- GET /api/v1/chair/get-chairs - Lista todas as cadeiras.
- GET /api/v1/chair/get-chair/{id} - Obtém uma cadeira pelo ID.
- POST /api/v1/chair/save - Cria uma nova cadeira.
- PUT /api/v1/chair/update/{id} - Atualiza uma cadeira existente.
- DELETE /api/v1/chair/delete/{id} - Exclui uma cadeira.

## Como Contribuir
- Faça um fork do repositório.
- Crie uma nova branch (git checkout -b feature/sua-feature).
- Commit suas mudanças (git commit -am 'Adicionei uma nova feature').
- Push para a branch (git push origin feature/sua-feature).
- Abra um Pull Request.

## Licença
Distribuído sob a licença MIT. Veja `LICENSE` para mais informações.
