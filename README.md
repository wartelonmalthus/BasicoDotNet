# Bernhoeft.GRT.Teste.Api

> API RESTful em **.NET 9.0** para gerenciamento de avisos (*notices*), demonstrando boas práticas como **Clean Architecture**, **CQRS** com **MediatR**, validação com **FluentValidation** e **testes automatizados**.

---

## 🚀 Tecnologias Utilizadas

- **.NET 9.0** — Framework principal para desenvolvimento da aplicação
- **ASP.NET Core** — Para construção da API RESTful
- **Entity Framework Core (EF Core)** — ORM para interação com o banco de dados (InMemory para desenvolvimento e testes)
- **MediatR** — Implementação do padrão CQRS (Command Query Responsibility Segregation) e Mediator
- **FluentValidation** — Biblioteca para validação robusta de objetos de requisição
- **Swagger/OpenAPI** — Para documentação interativa da API
- **xUnit & Moq** — Frameworks para testes unitários e de integração
- **FluentAssertions** — Para tornar as asserções dos testes mais legíveis
- **Bernhoeft.GRT.Core.EntityFramework** — Biblioteca interna para abstração de repositórios e contexto

---

## 🏗️ Arquitetura do Projeto

> O projeto segue princípios de **Clean Architecture** e **Domain-Driven Design (DDD)**, com uma clara separação de responsabilidades entre as camadas.

### Camadas

1. **Presentation** (`Bernhoeft.GRT.Teste.Api`)
   - Contém os controladores da API, responsáveis por receber as requisições HTTP e orquestrar a chamada aos handlers da camada de aplicação.

2. **Application** (`Bernhoeft.GRT.Teste.Application`)
   - Contém a lógica de negócio da aplicação, incluindo:
     - DTOs (Data Transfer Objects)
     - Handlers (MediatR Commands e Queries)
     - Validadores (FluentValidation)

3. **Domain** (`Bernhoeft.GRT.Teste.Domain`)
   - Define as entidades de domínio (`AvisoEntity`)
   - Interfaces de repositório (`IAvisoRepository`)
   - Regras de negócio essenciais

4. **Infra** (`Bernhoeft.GRT.Teste.Infra.Persistence.InMemory`)
   - Implementa a infraestrutura de persistência de dados
   - Utiliza banco de dados em memória para este projeto

---

## ⚙️ Como Rodar o Projeto

### Pré-requisitos

- **.NET 9.0 SDK** instalado ([Download aqui](https://dotnet.microsoft.com/download))
- Um editor de código:
  - *Visual Studio*
  - *Visual Studio Code*
  - *JetBrains Rider*

### Passos para Execução

**1. Clone o repositório**
```bash
git clone <URL_DO_SEU_REPOSITORIO>
cd Bernhoeft.GRT.Teste.Api

**1. Restaure as dependências**
```bash
dotnet restore

