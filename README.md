# Gestão de Usuários — Back-end

API REST para gerenciamento de usuários, desenvolvida como desafio técnico. O projeto aplica Clean Architecture, boas práticas de SOLID e padrões como Repository, Unit of Work e DTO.

---

## Tecnologias

| Tecnologia | Versão |
|---|---|
| .NET / ASP.NET Core | 10.0 |
| Entity Framework Core | 10.0.7 |
| SQL Server | 2022+ |
| AutoMapper | 16.1.1 |
| FluentValidation | 12.1.1 |
| BCrypt.Net-Next | 4.1.0 |
| Swashbuckle (Swagger) | 10.1.7 |

---

## Arquitetura

O projeto segue **Clean Architecture** dividida em 4 camadas:

```
src/
├── GestaoDeUsuarios.API/            # Camada de apresentação (controllers, middlewares, filtros)
├── GestaoDeUsuarios.Application/   # Casos de uso (AppService, DTOs, validações, mapeamentos)
├── GestaoDeUsuarios.Domain/        # Regras de negócio (entidades, interfaces, exceções)
└── GestaoDeUsuarios.Infrastructure/# Persistência (EF Core, repositórios, migrations)
```

---

## Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server 2022 (local ou via Docker)
- Docker + [Colima](https://github.com/abiosoft/colima) *(opcional, para Mac)*

---

## Como executar

### 1. Subir a infraestrutura (banco de dados)

```bash
# Inicia Docker/Colima + SQL Server
./scripts/iniciar-infra.sh
```

### 2. Criar o banco de dados

Execute o script SQL no SQL Server:

```bash
# Via sqlcmd ou sua IDE preferida
sqlcmd -S localhost -U sa -P 'SenhaAdmin9!' -i cria-banco.sql
```

Ou rode as migrations via EF Core:

```bash
cd src/GestaoDeUsuarios.API
dotnet ef database update
```

### 3. Iniciar a API

```bash
# Via script (usa o perfil LocalMacMock)
./scripts/iniciar-api.sh

# Ou manualmente
cd src/GestaoDeUsuarios.API
ASPNETCORE_ENVIRONMENT=LocalMacMock dotnet run
```

A API estará disponível em: `http://localhost:5120`

Documentação Swagger: `http://localhost:5120/swagger`

### 4. Parar os serviços

```bash
./scripts/parar-infra.sh
./scripts/finalizar-api.sh
```

---

## Configuração

A string de conexão é definida por ambiente:

| Arquivo | Ambiente |
|---|---|
| `appsettings.json` | Produção (LocalDB) |
| `appsettings.LocalMacMock.json` | Desenvolvimento local (Mac + Docker) |

Conexão padrão para desenvolvimento:

```
Server=localhost;Database=DB1677_ADA_GestaoDeUsuarios;User Id=sa;Password=SenhaAdmin9!;TrustServerCertificate=True;
```

---

## Endpoints

Base URL: `http://localhost:5120/api/v1/usuarios`

| Método | Rota | Descrição |
|--------|------|-----------|
| `POST` | `/` | Criar novo usuário |
| `GET` | `/` | Listar usuários ativos |
| `GET` | `/inativos` | Listar usuários inativos |
| `GET` | `/pesquisa/nome?query={nome}` | Buscar usuários por nome |
| `GET` | `/pesquisa/email?query={email}` | Buscar usuários por e-mail |
| `GET` | `/{id}` | Buscar usuário por ID |
| `PUT` | `/{id}` | Atualizar dados do usuário |
| `PATCH` | `/{id}/desativar` | Desativar usuário |

### Exemplo — Criar usuário

**Request:**
```json
POST /api/v1/usuarios
{
  "nome": "Alan Lima",
  "email": "alan@exemplo.com",
  "senha": "MinhaS3nha!",
  "cargo": "Engenheiro de Software"
}
```

**Response:**
```json
{
  "sucesso": true,
  "dados_resposta": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "nome": "Alan Lima",
    "email": "alan@exemplo.com",
    "cargo": "Engenheiro de Software",
    "ativo": true,
    "criado_em": "2024-01-01T00:00:00"
  },
  "erros": [],
  "timestamp_resposta": "01/01/2024 00:00:00",
  "tempo_da_resposta": "45 ms"
}
```

---

## Formato de resposta padrão

Todas as respostas seguem o envelope `ApiResponse`:

| Campo | Tipo | Descrição |
|-------|------|-----------|
| `sucesso` | bool | Indica se a operação foi bem-sucedida |
| `dados_resposta` | object | Payload da resposta |
| `erros` | array | Lista de mensagens de erro |
| `timestamp_resposta` | string | Data/hora da resposta (dd/MM/yyyy HH:mm:ss) |
| `tempo_da_resposta` | string | Tempo de processamento em ms |

---

## Observações

- As senhas são armazenadas com hash **BCrypt** — nunca em texto puro.
- Não há autenticação/autorização implementada (fora do escopo do desafio).
- A desativação de usuários é lógica (`ativo = false`); os registros não são removidos do banco.
- Respostas JSON utilizam nomenclatura **snake_case**.
