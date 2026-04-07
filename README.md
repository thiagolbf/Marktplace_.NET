# Markplace API

API REST para um marketplace, construída com ASP.NET Core 9, Entity Framework Core, Identity e autenticação JWT.

## Visão Geral

O projeto segue arquitetura em camadas, com separação entre domínio, aplicação, infraestrutura e controllers.

Principais recursos:
- Autenticação e autorização com JWT
- Gestão de roles com ASP.NET Identity (`Admin`, `Vendedor`, `Cliente`)
- Cadastro e atualização de perfil de vendedor e cliente
- CRUD de produtos e categorias
- Gestão de endereços do cliente
- Sistema de avaliações de produtos
- Validação de entrada com FluentValidation
- Middleware global para tratamento de exceções
- Documentação interativa via Swagger

## Tecnologias

- .NET 9 (`net9.0`)
- ASP.NET Core Web API
- Entity Framework Core 9
- SQL Server
- ASP.NET Core Identity
- JWT Bearer Authentication
- FluentValidation
- Swagger / OpenAPI (Swashbuckle)

## Estrutura do Projeto

- `Controllers/`: endpoints HTTP
- `Application/`: DTOs, interfaces e serviços
- `Domain/`: entidades, interfaces e exceções de domínio
- `Infrastructure/`: DbContext, repositórios, mapeamentos EF, unit of work, middleware
- `Migrations/`: migrações do banco

## Arquitetura e Padrões

### Repository Pattern

- O projeto usa um repositório genérico para operações base e repositórios específicos por agregado.
- A camada de aplicação depende de contratos (`IProdutoRepository`, `ICategoriaRepository`, etc.), e não de implementações concretas.
- Isso reduz acoplamento e melhora testabilidade e manutenção.

### Unit of Work

- O `UnitOfWork` centraliza a confirmação das alterações com `CommitAsync()`.
- Os serviços aplicam regras de negócio e apenas confirmam persistência ao final do fluxo.
- Esse padrão ajuda a manter consistência transacional entre múltiplas operações.

### Inversão de Dependência (DIP)

- O fluxo arquitetural segue `Controller -> Service -> Repository`.
- Cada camada depende de abstrações (interfaces), com implementações resolvidas via injeção de dependência no `Program.cs`.

## Conceitos de POO Aplicados

### Encapsulamento

As entidades expõem comportamentos de domínio (por exemplo, atualização de dados, alteração de status e ajustes de preço), evitando alterações de estado sem regra.

### Abstração

Interfaces nas camadas de aplicação e domínio definem contratos claros, desacoplando regra de negócio de detalhes de infraestrutura.

### Herança

As entidades compartilham base comum pela classe abstrata `Entity`, que centraliza identidade (`Id`).

### Composição

O modelo expressa relacionamentos entre entidades do domínio (produto/categoria, cliente/endereço, cliente/avaliação).

### Responsabilidade Única (SRP)

- Controllers: protocolo HTTP e resposta.
- Services: regras de negócio e orquestração.
- Repositories: acesso a dados.
- UnitOfWork: confirmação de persistência.

## Fluxo de Persistência (Resumo)

1. O controller recebe e valida a requisição.
2. O service executa regras de negócio.
3. O repository aplica operações no contexto.
4. O `UnitOfWork.CommitAsync()` confirma as alterações.
5. O middleware global trata exceções e padroniza erros.

## Endpoints Principais

Base URL local padrão (Swagger): `https://localhost:<porta>/swagger`

### Auth (`/api/Auth`)
- `POST /register`: registra usuário e associa role existente
- `POST /login`: autentica e retorna token JWT
- `POST /forcar-logout`: invalida token atual via `SecurityStamp` (requer autenticação)

### Roles (`/api/Role`)
- `POST /`: cria role
- `GET /`: lista roles
- `DELETE /{roleName}`: remove role

### Vendedor (`/api/Vendedor`)
- `POST /completar`: completa cadastro de vendedor
- `GET /meu-perfil`: obtém perfil autenticado
- `PATCH /meu-perfil`: atualiza perfil autenticado
- `PATCH /{id}/status`: ativa/inativa vendedor (`Admin`)

### Cliente (`/api/Cliente`)
- `POST /completar`: completa cadastro de cliente
- `GET /meu-perfil`: obtém perfil autenticado
- `PATCH /meu-perfil`: atualiza perfil autenticado
- `PATCH /{id}/status`: ativa/inativa cliente (`Admin`)

### Produto (`/api/Produto`)
- `GET /`: lista produtos
- `GET /{id}`: busca produto por id
- `POST /`: cria produto (`Vendedor`)
- `PUT /{id}`: atualiza nome/descrição (`Vendedor`)
- `PATCH /{id}/preco`: atualiza preço (`Vendedor`)
- `DELETE /{id}`: desativa produto (`Vendedor`)

### Categoria (`/api/Categoria`)
- `GET /`: lista categorias (`Vendedor`)
- `GET /{id}`: busca categoria por id (`Vendedor`)
- `POST /`: cria categoria (`Vendedor`)
- `PATCH /{id}`: atualiza categoria (`Vendedor`)
- `DELETE /{id}`: remove categoria (`Admin`)

### Endereço (`/api/Endereco`)
- `GET /`: lista endereços do cliente autenticado
- `GET /{id}`: busca endereço por id
- `POST /`: cria endereço
- `PUT /{id}`: atualiza endereço
- `DELETE /{id}`: remove endereço

### Avaliação (`/api/Avaliacao`)
- `GET /produto/{produtoId}`: lista avaliações do produto
- `POST /`: cria avaliação (`Cliente`)
- `PUT /{id}`: atualiza avaliação (`Cliente`)
- `DELETE /{id}`: remove avaliação (`Cliente`)

## Licença

Projeto para fins educacionais/estudo.
