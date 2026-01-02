# EasyDonate API

ASP.NET Core Web API built with Clean Architecture for study and learning purposes

###

[English](#en) | [Português](#pt-BR)

<a name="pt-BR"></a>
## 🇧🇷 Português

### 📖 Sobre

Este projeto consiste em uma ASP.NET Core Web API desenvolvida com base nos princípios da Clean Architecture (Arquitetura Limpa), com foco em boas práticas de arquitetura de software, organização de código e separação de responsabilidades.

A aplicação foi criada inicialmente como Trabalho de Conclusão de Curso (TCC) da Unicesumar – Londrina, cujo objetivo era o desenvolvimento de um aplicativo mobile voltado para doações, permitindo que ONGs se cadastrassem para receber diferentes tipos de doações.

Posteriormente, o projeto passou por um processo de refatoração completa, resultando nesta API, que aplica padrões modernos de desenvolvimento, tornando o código mais manutenível, escalável e testável.

Refatoração feita por ***Guilherme Rodrigues***.

---

### 🚀 Funcionalidades

A API fornece um sistema CRUD completo para um aplicativo de doações, incluindo:
- Autenticação e autorização
- Controle de acesso por roles (Donor, Ong e Admin)
- Cadastro e gerenciamento de ONGs e doadores
- Gerenciamento de endereços
- Registro e consulta de doações

---

### 🎯 Objetivo

O principal objetivo deste projeto é educacional, servindo como um estudo prático de:
- Clean Architecture
- ASP.NET Core Web API
- Boas práticas de desenvolvimento backend
- Estruturação de APIs REST

---

### 🚀 Tecnologias

#### Frameworks & Runtime
- **.NET 9.0** - Framework .NET (Plataforma base da aplicação)
- **ASP.NET Core** - Framework para desenvolvimento de Web APIs
- **Entity Framework Core 9.0** - ORM para acesso ao banco de dados

#### Bibliotecas & Pacotes
- **AutoMapper 16.0.0** - Mapeamento objeto-para-objeto
- **BCrypt.Net-Core 1.6.0** - Criptografia e hash de senhas
- **Microsoft.AspNetCore.Authentication.JwtBearer 9.0.11** - Autenticação via JWT
- **System.IdentityModel.Tokens.Jwt 8.15.0** - Geração e validação de tokens JWT
- **Pomelo.EntityFrameworkCore.MySql 9.0.10** - Provider MySQL para EF Core
- **Scalar.AspNetCore 2.11.6** - Documentação da API

#### Banco de Dados
- **MySQL** - Banco de dados relacional

#### Padrões de Projeto
- **Clean Architecture** - Separação de responsabilidades
- **Unit of Work** - Gerenciamento de transações

---

### 👥 Roles (Donor, Ong, Admin)

1. **🌍 Público (Qualquer usuário / Não autenticado)**

| Método | Endpoint     |
| ------ | ------------ |
| POST   | `/api/Ong`   |
| POST   | `/api/Donor` |
| POST   | `/api/Auth`  |
|                       |


2. **🔄 Ações Comuns (Ong e Donor)**

| Método | Endpoint                             | Descrição         |
| ------ | ------------------------------------ | ----------------- |
| PATCH  | `/api/User/Inactivate/Email/{email}` | Inativa usuário   |
| PATCH  | `/api/User/Activate/Email/{email}`   | Ativa usuário     |
| GET    | `/api/Address/Ong/{id}`              | Endereço da Ong   |
| GET    | `/api/Address/Donor/{id}`            | Endereço do Donor |
| POST   | `/api/Address`                       | Cria endereço     |
|                                                                   |


2. **🏢 Ong**

| Método | Endpoint                    | Descrição                  |
| ------ | --------------------------- | -------------------------- |
| GET    | `/api/Ong`                  | Lista todas as Ongs        |
| GET    | `/api/Ong/{id}`             | Busca Ong por ID           |
| PATCH  | `/api/Ong/{id}`             | Atualiza dados da Ong      |
| GET    | `/api/Donor/{id}`           | Busca Donor por ID         |
| GET    | `/api/Donation/Ong/{ongId}` | Doações recebidas pela Ong |
|                                                                   |


3. **🙋 Donor**

| Método | Endpoint                        | Descrição          |
| ------ | ------------------------------- | ------------------ |
| GET    | `/api/Ong`                      | Lista Ongs         |
| GET    | `/api/Ong/{id}`                 | Detalhes da Ong    |
| GET    | `/api/Donor/{id}`               | Dados do Donor     |
| PATCH  | `/api/Donor/{id}`               | Atualiza Donor     |
| GET    | `/api/Donation/Donor/{donorId}` | Doações realizadas |
| POST   | `/api/Donation`                 | Cria doação        |
|                                                               |


4. **🛠️ Admin**

| Permissão                       |
| ------------------------------- |
| Acesso a **todos os endpoints** |
|                                 |

---

### 📥 Instalação

1. **Clone o repositório**

```bash
git clone https://github.com/guilherme-rodrigues-de-queiroz/EasyDonate.git
cd EasyDonate
```

2. **Configure a conexão com o banco de dados**

Edite o arquivo `appsettings.json` em `Presentation/EasyDonate.API/`:

```json
{
  "ConnectionStrings": {
    "MySQL": "Server=localhost;Database=NOME_DO_BANCO;Uid=SEU_USUARIO;Pwd=SUA_SENHA;"
  }
}
```

3. **Configure o pepper para hash de senhas**

Gere um token de 64 bits, pode utilizar o seguinte site para isso: https://jwtsecrets.com/

Edite o arquivo `appsettings.json` em `Presentation/EasyDonate.API/`:

```json
{
  "Security": {
    "Pepper": "SEU_TOKEN_64_BITS"
  }
}
```

4. **Configure o token JWT**

Gere um token de 256 bits, pode utilizar o seguinte site para isso: https://jwtsecrets.com/

Edite o arquivo `appsettings.json` em `Presentation/EasyDonate.API/`:

```json
{
  "Jwt": {
    "key": "SEU_TOKEN_256_bits",
    "Issuer": "EasyDonate",
    "Audience": "easydonate_app",
    "ExpirationMinutes": 30
  },
}
```

---

5. **Migrations**

Como criar migrations com o entity framework?

Abra o CMD do windows, acesse o diretório do projeto EasyDonate e execute

```bash
dotnet ef migrations add Initial --project Infrastructure/EasyDonate.Persistence --startup-project Presentation/EasyDonate.API
```

Remover migrations

```bash
dotnet ef migrations remove --project Infrastructure/EasyDonate.Persistence --startup-project Presentation/EasyDonate.API
```

Criar o banco de dados

```bash
dotnet ef database update --project Infrastructure/EasyDonate.Persistence --startup-project Presentation/EasyDonate.API
```

Deletar o banco de dados

```bash
dotnet ef database drop --project Infrastructure/EasyDonate.Persistence --startup-project Presentation/EasyDonate.API
```

6. **Restaure as dependências**

```bash
dotnet restore
```

7. **Compile o projeto**

```bash
dotnet build
```

---

### ▶️ Rodando o projeto

1. **Acesse o diretório do projeto pelo CMD**

```bash
cd Presentation/EasyDonate.API
```

2. **Roda a API**

```bash
dotnet run
```

3. **A API vai estar disponível em algum dos endereços abaixo:**

- HTTPS: `https://localhost:7xxx/Scalar` (porta pode variar)
- HTTP: `http://localhost:5xxx/Scalar` (porta pode variar)

O CMD vai abrir ao rodar a aplicação e o endereço da API estará nele.

---

### 📁 Estrutura do Projeto

```
EasyDonate/
│
├── .github/
│   └── workflows/                  # Pipelines CI/CD
│
├── Core/
│   ├── EasyDonate.Domain/          # Camada de domínio (regra de negócio)
│   │   ├── Entities/               # Entidades do domínio
│   │   ├── Enums/                  # Enumerações
│   │   └── Exceptions/             # Exceções de domínio
│   │
│   └── EasyDonate.Application/     # Camada de aplicação
│       ├── DTOs/
│       │   ├── Requests/           # DTOs de entrada
│       │   ├── Responses/          # DTOs de saída
│       │   └── Mappings/           # Configurações do AutoMapper
│       ├── Interfaces/             # Contratos (services, repositories, etc.)
│       ├── Services/               # Casos de uso / regras de aplicação
│       ├── Validations/            # Validações de negócio
│       └── DependencyInjection.cs  # Injeção de dependências da camada
│
├── Infrastructure/
│   └── EasyDonate.Persistence/     # Camada de infraestrutura
│       ├── Context/                # DbContext (EF Core)
│       ├── Migrations/             # Migrations do banco
│       ├── Repositories/           # Implementações de repositórios
│       ├── Security/               # Autenticação, JWT, hash de senhas
│       └── DependencyInjection.cs  # Injeção de dependências da infraestrutura
│
├── Presentation/
│   └── EasyDonate.API/             # Camada de apresentação (Web API)
│       ├── Controllers/            # Endpoints da API
│       ├── Extensions/             # Configurações e middlewares
│       ├── appsettings.json        # Configurações da aplicação
│       └── Program.cs              # Bootstrap da API (Processo de inicialização da API)
│
└── README.md
```

---

### ✨ Funcionalidades

#### Implementadas
- ✅ Autenticação/Autorização
- ✅ Clean Architecture com separação clara de camadas
- ✅ Configuração de política CORS
- ✅ CRUD completo
- ✅ Documentação da API com Scalar
- ✅ Integração com banco de dados MySQL
- ✅ Mapeamento automático com AutoMapper
- ✅ Unit of Work

#### Não Implementadas (Melhorias Futuras)
- ⚠️ Criação automática do banco de dados
- ⚠️ Paginação
- ⚠️ Sistema de Logging
- ⚠️ Suporte ao Docker
- ⚠️ Testes Unitários
- ⚠️ Versionamento da API

---

### 👨‍💻 Autores

Projeto desenvolvido por:
- <a href="https://github.com/guilherme-rodrigues-de-queiroz">Guilherme Rodrigues</a>
- <a href="https://github.com/H0wZy">Marcos “H0wZy” Junior</a>

---

<a name="en"></a>
## 🇺🇸 English

### 📖 About

This project consists of an ASP.NET Core Web API developed based on the principles of Clean Architecture, focusing on good software architecture practices, code organization, and separation of responsibilities.

The application was initially created as a Final Graduation Project (TCC) at Unicesumar – Londrina, whose objective was the development of a mobile application focused on donations, allowing NGOs to register to receive different types of donations.

Later, the project went through a complete refactoring process, resulting in this API, which applies modern development standards, making the code more maintainable, scalable, and testable.

Refactoring done by ***Guilherme Rodrigues***.

---

### 🚀 Features

The API provides a complete CRUD system for a donation application, including:
- Authentication and authorization
- Role-based access control (Donor, Ong, and Admin)
- NGO and donor registration and management
- Address management
- Donation registration and consultation

---

### 🎯 Purpose

The main purpose of this project is educational, serving as a practical study of:
- Clean Architecture
- ASP.NET Core Web API
- Backend development best practices
- REST API structuring

---

### 🚀 Technologies

#### Frameworks & Runtime
- **.NET 9.0** – .NET framework (base platform of the application)
- **ASP.NET Core** – Framework for Web API development
- **Entity Framework Core 9.0** – ORM for database access

#### Libraries & Packages
- **AutoMapper 16.0.0** - Object-to-object mapping
- **BCrypt.Net-Core 1.6.0** - Password encryption and hashing
- **Microsoft.AspNetCore.Authentication.JwtBearer 9.0.11** - JWT authentication
- **System.IdentityModel.Tokens.Jwt 8.15.0** - JWT token generation and validation
- **Pomelo.EntityFrameworkCore.MySql 9.0.10** - MySQL provider for EF Core
- **Scalar.AspNetCore 2.11.6** - API documentation

#### Banco de Dados
- **MySQL** - Relational database

#### Padrões de Projeto
- **Clean Architecture** - Separation of responsibilities
- **Unit of Work** - Transaction management

---

### 👥 Roles (Donor, Ong, Admin)

1. **🌍 Public (Any user / Not authenticated)**

| Method | Endpoint     |
| ------ | ------------ |
| POST   | `/api/Ong`   |
| POST   | `/api/Donor` |
| POST   | `/api/Auth`  |
|                       |


2. **🔄 Common Actions (Ong and Donor)**

| Method | Endpoint                             | Description     |
| ------ | ------------------------------------ | --------------- |
| PATCH  | `/api/User/Inactivate/Email/{email}` | Inactivate user |
| PATCH  | `/api/User/Activate/Email/{email}`   | Activate user   |
| GET    | `/api/Address/Ong/{id}`              | Ong address     |
| GET    | `/api/Address/Donor/{id}`            | Donor address   |
| POST   | `/api/Address`                       | Create address  |
|                                                                 |


2. **🏢 Ong**

| Method | Endpoint                    | Description                   |
| ------ | --------------------------- | ----------------------------- |
| GET    | `/api/Ong`                  | List all Ongs                 |
| GET    | `/api/Ong/{id}`             | Get Ong by ID                 |
| PATCH  | `/api/Ong/{id}`             | Update Ong data               |
| GET    | `/api/Donor/{id}`           | Get Donor by ID               |
| GET    | `/api/Donation/Ong/{ongId}` | Donations received by the Ong |
|                                                                      |


3. **🙋 Donor**

| Method | Endpoint                        | Description     |
| ------ | ------------------------------- | --------------- |
| GET    | `/api/Ong`                      | List Ongs       |
| GET    | `/api/Ong/{id}`                 | Ong details     |
| GET    | `/api/Donor/{id}`               | Donor data      |
| PATCH  | `/api/Donor/{id}`               | Update Donor    |
| GET    | `/api/Donation/Donor/{donorId}` | Donations made  |
| POST   | `/api/Donation`                 | Create donation |
|                                                            |


4. **🛠️ Admin**

| Permission                  |
| --------------------------- |
| Access to **all endpoints** |
|                             |

---

### 📥 Installation

1. **Clone the repository**

```bash
git clone https://github.com/guilherme-rodrigues-de-queiroz/EasyDonate.git
cd EasyDonate
```

2. **Configure the database connection**

Edit the `appsettings.json` file in `Presentation/EasyDonate.API/`:

```json
{
  "ConnectionStrings": {
    "MySQL": "Server=localhost;Database=DB_NAME;Uid=USER;Pwd=PASSWORD;"
  }
}
```

3. **Configure the pepper for password hashing**

Generate a 64-bit token. You can use the following site for this: https://jwtsecrets.com/

Edit the `appsettings.json` file in `Presentation/EasyDonate.API/`:

```json
{
  "Security": {
    "Pepper": "YOUR_64_BITS_TOKEN"
  }
}
```

4. **Configure the JWT token**

Generate a 256-bit token. You can use the following site for this: https://jwtsecrets.com/

Edit the `appsettings.json` file in `Presentation/EasyDonate.API/`:

```json
{
  "Jwt": {
    "key": "YOUR_256_BITS_TOKEN",
    "Issuer": "EasyDonate",
    "Audience": "easydonate_app",
    "ExpirationMinutes": 30
  },
}
```

---

5. **Migrations**

How to create migrations using Entity Framework?

Open the Windows CMD, navigate to the EasyDonate project directory, and run:

```bash
dotnet ef migrations add Initial --project Infrastructure/EasyDonate.Persistence --startup-project Presentation/EasyDonate.API
```

Remove migrations

```bash
dotnet ef migrations remove --project Infrastructure/EasyDonate.Persistence --startup-project Presentation/EasyDonate.API
```

Create database

```bash
dotnet ef database update --project Infrastructure/EasyDonate.Persistence --startup-project Presentation/EasyDonate.API
```

Delete database

```bash
dotnet ef database drop --project Infrastructure/EasyDonate.Persistence --startup-project Presentation/EasyDonate.API
```

6. **Restore dependencies**

```bash
dotnet restore
```

7. **Build the project**

```bash
dotnet build
```

---

### ▶️ Running the project

1. **Access the project directory using CMD**

```bash
cd Presentation/EasyDonate.API
```

2. **Run API**

```bash
dotnet run
```

3. **The API will be available at one of the addresses below::**

- HTTPS: `https://localhost:7xxx/Scalar` (port may vary)
- HTTP: `http://localhost:5xxx/Scalar` (port may vary)

The CMD will open when running the application, and the API address will be displayed there.

---

### 📁 Project Structure

```
EasyDonate/
│
├── .github/
│   └── workflows/                  # Pipelines CI/CD
│
├── Core/
│   ├── EasyDonate.Domain/          # Domain layer (business rules)
│   │   ├── Entities/               # Domain entities
│   │   ├── Enums/                  # Enumerations
│   │   └── Exceptions/             # Domain exceptions
│   │
│   └── EasyDonate.Application/     # CApplication layer
│       ├── DTOs/
│       │   ├── Requests/           # Input DTOs
│       │   ├── Responses/          # Output DTOs
│       │   └── Mappings/           # AutoMapper configurations
│       ├── Interfaces/             # Contracts (services, repositories, etc.)
│       ├── Services/               # Use cases / application rules
│       ├── Validations/            # Business validations
│       └── DependencyInjection.cs  # Layer dependency injection
│
├── Infrastructure/
│   └── EasyDonate.Persistence/     # Infrastructure layer
│       ├── Context/                # DbContext (EF Core)
│       ├── Migrations/             # Database migrations
│       ├── Repositories/           # Repository implementations
│       ├── Security/               # Authentication, JWT, password hashing
│       └── DependencyInjection.cs  # Layer dependency injection
│
├── Presentation/
│   └── EasyDonate.API/             # Presentation layer (Web API)
│       ├── Controllers/            # API endpoints
│       ├── Extensions/             # Configurations and middlewares
│       ├── appsettings.json        # Application settings
│       └── Program.cs              # API bootstrap (API initialization process)
│
└── README.md
```

---

### ✨ Features

#### Implemented
- ✅ Authentication / Authorization
- ✅ Clean Architecture with clear layer separation
- ✅ CORS policy configuration
- ✅ Complete CRUD
- ✅ API documentation with Scalar
- ✅ MySQL database integration
- ✅ Automatic mapping with AutoMapper
- ✅ Unit of Work

#### Not Implemented (Future Improvements)
- ⚠️ Automatic database creation
- ⚠️ Pagination
- ⚠️ Logging system
- ⚠️ Docker support
- ⚠️ Unit tests
- ⚠️ API versioning

---

### 👨‍💻 Authors

Project developed by:
- <a href="https://github.com/guilherme-rodrigues-de-queiroz">Guilherme Rodrigues</a>
- <a href="https://github.com/H0wZy">Marcos “H0wZy” Junior</a>