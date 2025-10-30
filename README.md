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
```

**2. Restaure as dependências**
```bash
dotnet restore
```

**3. Execute a aplicação**
```bash
dotnet run --project "1-Presentation/Bernhoeft.GRT.Teste.Api/Bernhoeft.GRT.Teste.Api.csproj"
```

**4. Acesse a API**
- API: <https://localhost:7001> *(ajuste a porta se necessário)*
- Swagger UI: <https://localhost:7001/swagger>

> 💡 **Dica:** Abra seu navegador e vá para a URL do Swagger para interagir com a API através da interface visual.

---

## 🧪 Testes Automatizados

> O projeto inclui **testes unitários** e **testes de integração** para garantir a qualidade e o comportamento esperado da aplicação.

### Estratégia de Testes

| Tipo de Teste | Foco | Ferramentas |
|---------------|------|-------------|
| **Testes Unitários** | Lógica de negócio isolada (Handlers, Validators) | xUnit, Moq |
| **Testes de Integração** | Fluxo completo da API (Controllers, Handlers, Repositórios) | Microsoft.AspNetCore.Mvc.Testing |

### Como Executar os Testes

**Navegue até a pasta de testes:**
```bash
cd Bernhoeft.GRT.Teste.Tests
```

**Execute os testes:**
```bash
dotnet test
```

**Execute com cobertura de código:**
```bash
dotnet test --collect:"XPlat Code Coverage"
```

---

## 📖 Endpoints da API (Avisos)

> Todos os endpoints estão sob a rota base `/avisos`.

### **POST** `/avisos`
Cria um novo aviso.

**Request Body:**
```json
{
  "titulo": "Título do Aviso",
  "mensagem": "Conteúdo detalhado da mensagem do aviso."
}
```

**Response (201 Created):**
```json
{
  "id": 1,
  "titulo": "Título do Aviso",
  "mensagem": "Conteúdo detalhado da mensagem do aviso.",
  "dataCriacao": "2025-10-30T10:00:00Z",
  "dataAlteracao": null,
  "ativo": true
}
```

**Response (400 Bad Request):**
```json
{
  "statusCode": 400,
  "messages": [
    "Titulo é obrigatório.",
    "Mensagem deve ter no máximo 1000 caracteres."
  ],
  "data": null
}
```

---

### **GET** `/avisos`
Lista todos os avisos com suporte a **paginação** e **filtros**.

**Query Parameters:**

| Parâmetro | Tipo | Default | Descrição |
|-----------|------|---------|-----------|
| `PageNumber` | int | 1 | Número da página |
| `PageSize` | int | 10 | Quantidade de itens por página |
| `Titulo` | string | - | Filtra avisos por título (parcial) |
| `Mensagem` | string | - | Filtra avisos por mensagem (parcial) |
| `Ativo` | bool | - | Filtra avisos ativos/inativos |

**Exemplo de Request:**
```http
GET /avisos?PageNumber=1&PageSize=5&Titulo=Teste&Ativo=true
```

**Response (200 OK):**
```json
{
  "statusCode": 200,
  "messages": [],
  "data": {
    "items": [
      {
        "id": 1,
        "titulo": "Aviso de Teste 1",
        "mensagem": "Mensagem do aviso 1",
        "dataCriacao": "2025-10-30T10:00:00Z",
        "dataAlteracao": null,
        "ativo": true
      }
    ],
    "pageNumber": 1,
    "pageSize": 5,
    "totalCount": 1,
    "totalPages": 1,
    "hasNextPage": false,
    "hasPreviousPage": false
  }
}
```

---

### **GET** `/avisos/{id}`
Obtém um aviso específico pelo ID.

**URL Parameter:**
- `id` (int) — Identificador do aviso

**Exemplo de Request:**
```http
GET /avisos/1
```

**Response (200 OK):**
```json
{
  "statusCode": 200,
  "messages": [],
  "data": {
    "id": 1,
    "titulo": "Aviso de Teste 1",
    "mensagem": "Mensagem do aviso 1",
    "dataCriacao": "2025-10-30T10:00:00Z",
    "dataAlteracao": null,
    "ativo": true
  }
}
```

**Response (404 Not Found):**
> Se o aviso não for encontrado.

---

### **PUT** `/avisos/{id}`
Atualiza um aviso existente.

**URL Parameter:**
- `id` (int) — Identificador do aviso

**Request Body:**
```json
{
  "id": 1,
  "titulo": "Título Atualizado",
  "mensagem": "Nova mensagem do aviso."
}
```

**Response (200 OK):**
```json
{
  "statusCode": 200,
  "messages": [],
  "data": {
    "id": 1,
    "titulo": "Título Atualizado",
    "mensagem": "Nova mensagem do aviso.",
    "dataCriacao": "2025-10-30T10:00:00Z",
    "dataAlteracao": "2025-10-30T11:30:00Z",
    "ativo": true
  }
}
```

**Response (400 Bad Request):**
> Se a validação falhar.

**Response (404 Not Found):**
> Se o aviso não for encontrado.

---

### **DELETE** `/avisos/{id}`
Realiza um ***soft delete*** de um aviso, marcando-o como inativo (`Ativo = false`).

**URL Parameter:**
- `id` (int) — Identificador do aviso

**Exemplo de Request:**
```http
DELETE /avisos/1
```

**Response (200 OK):**
```json
{
  "statusCode": 200,
  "messages": [],
  "data": {
    "id": 1
  }
}
```

**Response (404 Not Found):**
> Se o aviso não for encontrado.

---

## 💡 Decisões de Design e Boas Práticas

### CQRS Leve com MediatR
> A separação de comandos e queries através do **MediatR** promove um código mais organizado, escalável e fácil de manter, permitindo lógicas de validação e tratamento específicas para cada tipo de operação.

### Validação Robusta com FluentValidation
> Garante que os dados de entrada da API sejam sempre válidos, fornecendo feedback claro e detalhado ao cliente.

### Padrão Repositório
> Abstrai a camada de persistência, tornando a aplicação independente da tecnologia de banco de dados subjacente.

### Objetos de Valor (DTOs)
> Utilizados para transferir dados entre as camadas, garantindo que a API não exponha diretamente as entidades de domínio.

### Tratamento de Erros Padronizado
> O uso de `IOperationResult` e um tratamento de exceções consistente garante que as respostas da API sejam previsíveis e informativas.

### Testabilidade
> A arquitetura e o uso de injeção de dependência facilitam a escrita de testes unitários e de integração, cobrindo a maior parte da lógica da aplicação.

### Banco de Dados em Memória
> Para agilizar o desenvolvimento e os testes, um banco de dados em memória é utilizado, com seeding inicial para dados de exemplo.

---

## ⏭️ Próximos Passos (Melhorias Futuras)

> Se houvesse mais tempo, as seguintes melhorias seriam implementadas:

- [ ] **Autenticação e Autorização** — Implementar JWT (JSON Web Tokens) para proteger os endpoints da API
- [ ] **Banco de Dados Real** — Migrar de um banco de dados em memória para SQL Server ou PostgreSQL com migrações do EF Core
- [ ] **Mapeamento Automático** — Utilizar AutoMapper para simplificar o mapeamento entre DTOs e entidades
- [ ] **Cache** — Implementar cache para endpoints de leitura frequente para melhorar a performance
- [ ] **Observabilidade** — Adicionar telemetria com OpenTelemetry para monitoramento e rastreamento distribuído
- [ ] **Docker Compose** — Configurar um ambiente Docker Compose para facilitar a execução da API e de um banco de dados real

---

## 📦 Estrutura de Pastas

```
.
├── 1-Presentation/
│   └── Bernhoeft.GRT.Teste.Api/          # Camada de apresentação (Controllers)
├── 2-Application/
│   └── Bernhoeft.GRT.Teste.Application/   # Camada de aplicação (Handlers, DTOs, Validators)
├── 3-Domain/
│   └── Bernhoeft.GRT.Teste.Domain/        # Camada de domínio (Entities, Interfaces)
├── 4-Infra/
│   └── Bernhoeft.GRT.Teste.Infra.Persistence.InMemory/  # Infraestrutura (Repositórios, DbContext)
└── tests/
    └── Bernhoeft.GRT.Teste.Tests/         # Testes unitários e de integração
```

---

## 📝 Licença

Este projeto está sob a licença **MIT**. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.

---

## 👨‍💻 Autor

Desenvolvido por **[Seu Nome]**

- GitHub: [@seu-usuario](https://github.com/seu-usuario)
- LinkedIn: [Seu Nome](https://www.linkedin.com/in/seu-perfil)
- Email: seu.email@exemplo.com

---

## 🤝 Contribuindo

Contribuições são bem-vindas! Para contribuir:

1. Faça um **fork** do projeto
2. Crie uma **branch** para sua feature (`git checkout -b feature/MinhaFeature`)
3. Faça **commit** das suas alterações (`git commit -m 'Adiciona MinhaFeature'`)
4. Faça **push** para a branch (`git push origin feature/MinhaFeature`)
5. Abra um **Pull Request**

---

## 📞 Suporte

Se você encontrar algum problema ou tiver alguma dúvida, por favor:

- Abra uma [Issue](https://github.com/seu-usuario/seu-repositorio/issues)
- Entre em contato via email: seu.email@exemplo.com

---

<div align="center">

**⭐ Se este projeto foi útil para você, considere dar uma estrela! ⭐**

Desenvolvido com ❤️ usando **.NET 9.0**

</div>
