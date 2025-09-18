## 🚀 MotoSecurityX — Challenge (2TDS 2025)

Clean Architecture + DDD + EF Core + Swagger (ASP.NET Core 8)

API para controle e monitoramento de motos e pátios. O projeto aplica Clean Architecture, DDD (Entidades ricas + VO) e boas práticas de Clean Code.

## 👥 Integrantes do Grupo

Caio Henrique – RM: 554600
Carlos Eduardo – RM: 555223
Antônio Lino – RM: 554518

## 🎯 Objetivo e Domínio

O domínio simula operações da Mottu:

  Pátios (unidades) recebem/armazenam motos.

  Motos possuem Placa (Value Object), Modelo e podem estar dentro ou fora de um pátio.

  Regras: placa única, entrada/saída de motos no pátio com métodos de comportamento no domínio.

  Benefício de negócio: visibilidade de ativos, rastreio de alocação por pátio e base para operações/logística.

## 🧭 Arquitetura (Camadas)

CP4.MotoSecurityX.Api/ -> Controllers, Program, Swagger, appsettings 
CP4.MotoSecurityX.Application/ -> Use cases (Handlers), DTOs 
CP4.MotoSecurityX.Domain/ -> Entidades, Value Objects, Interfaces (Repos) 
CP4.MotoSecurityX.Infrastructure/ -> EF Core (DbContext, Migrations), Repos EF, DI

- Princípios aplicados

  Inversão de Dependência: interfaces de repositório no Domain; implementações na Infrastructure.
  
  Baixo acoplamento entre camadas; a API não referencia EF diretamente.
  
  Regra de negócio no domínio (métodos em entidades) + use cases no Application.

  Clean Code: SRP/DRY/KISS/YAGNI, nomes claros, controllers finos.

## 🧩 Modelagem de Domínio (DDD)

- Entidades:

    Moto (rich model): EntrarNoPatio(Guid), SairDoPatio(), AtualizarModelo(string)

    Patio (Agregado Raiz): mantém coleção de motos via métodos AdmitirMoto(Moto) e RemoverMoto(Moto)
- Value Object:

    Placa (mapeada como owned type no EF, índice único na tabela de Motos)

✅ Status atual: 2 entidades implementadas (Moto, Patio) + 1 VO (Placa).

🧱 Backlog para nota máxima ABD (3 entidades + CRUD completo): adicionar a 3ª entidade (ex.: Ocorrencia ou Manutencao) com CRUD e paginação.

## 🔧 Requisitos

.NET 8 SDK

(Opcional) dotnet-ef como ferramenta local (manifesto já em .config/dotnet-tools.json)

SQLite (dev) ou Azure SQL (cloud)

## ▶️ Como executar localmente

- Na raiz do repositório:

  # Restaurar e compilar
    
    dotnet restore
    dotnet build

  # (uma vez) restaurar a ferramenta local dotnet-ef
    
    dotnet tool restore

  # Criar/atualizar o banco Sqlite (se ainda não existir)
    dotnet ef database update `
      -p .\CP4.MotoSecurityX.Infrastructure\ `
      -s .\CP4.MotoSecurityX.Api\

  # Subir a API
    dotnet run --project .\CP4.MotoSecurityX.Api\

    Swagger: a URL exata aparece no terminal (ex.: http://localhost:5102/swagger).

    Banco: Sqlite (Data Source=motosecurityx.db no appsettings.json da API).

## 🌐 Endpoints (exemplos)
  
- Criar Pátio POST /api/patios

{
  "nome": "Pátio Central",
  "endereco": "Rua 1"
}

- Criar Moto POST /api/motos

{
  "placa": "ABC1D23",
  "modelo": "Mottu 110i"
}
- Listar Motos GET /api/motos

  Obter Moto por Id GET /api/motos/{id}

  Mover Moto para um Pátio POST /api/motos/{id}/mover

  { "patioId": "<GUID do pátio>" }
  
Todos os endpoints podem ser exercitados via Swagger.

## 🗃️ Persistência & Migrations

  EF Core 8 + SQLite (dev), Azure SQL (cloud)

  Migration InitialCreate versionada em CP4.MotoSecurityX.Infrastructure/Data/Migrations.

  Connection string em appsettings.json (ConnectionStrings:Default).

  Para apontar para outro banco (ex.: Azure SQL) sem mexer no código:

-  Windows/PowerShell

  $env:ConnectionStrings__Default="Server=tcp:<server>.database.windows.net,1433;Database=<db>;User ID=<user>;Password=<pwd>;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"


- Linux/macOS

  export ConnectionStrings__Default="Server=tcp:...;Database=...;User ID=...;Password=...;Encrypt=True;"


- Aplicar migrations usando a connection string acima:

dotnet ef database update \
  -p ./CP4.MotoSecurityX.Infrastructure/ \
  -s ./CP4.MotoSecurityX.Api/

## 📜 Swagger / OpenAPI

  Swagger UI habilitado em Development.

  Todos os endpoints e modelos (DTOs) disponíveis em /swagger.

  Exemplos de payloads incluídos nos blocos acima.

- ⚠️ Backlog ABD: adicionar exemplos/descrições customizadas no Swagger (ex.: SwaggerGen com ExampleFilters) e HATEOAS na representação para pontuação máxima.

## 🧼 Clean Code

SRP/DRY/KISS/YAGNI

Controllers finos, uso de DTOs e handlers

Nomes claros e métodos pequenos

## 🗒️ DDL (script_bd.sql)

-- Patios (Agregado Raiz)
CREATE TABLE Patios (
  Id        UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
  Nome      NVARCHAR(120)    NOT NULL,
  Endereco  NVARCHAR(200)    NOT NULL
);

-- Motos
CREATE TABLE Motos (
  Id             UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
  Placa          NVARCHAR(10)     NOT NULL UNIQUE,
  Modelo         NVARCHAR(120)    NOT NULL,
  DentroDoPatio  BIT              NOT NULL DEFAULT(0),
  PatioId        UNIQUEIDENTIFIER NULL,
  CONSTRAINT FK_Motos_Patios FOREIGN KEY (PatioId) REFERENCES Patios(Id) ON DELETE SET NULL
);

## 📄 Licença

Uso educacional/acadêmico.

## 🌟 Propósito

“Código limpo sempre parece que foi escrito por alguém que se importa.” — Uncle Bob
