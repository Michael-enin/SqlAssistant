# SQL Assistant

SQL Assistant is a .NET Web API that automatically generates SQL Server stored procedures from structured JSON requests.

The goal of the project is to simplify and standardize SQL development by generating consistent, production-ready stored procedures for:

- SELECT
- INSERT
- UPDATE
- DELETE
- MERGE / UPSERT (planned)

---

## Features

### Select Procedure Generation

Generate stored procedures with:

- Column selection
- Table aliases
- Multiple joins
- Dynamic filtering
- Standard SQL formatting

Example:

```sql
CREATE PROCEDURE dbo.GetSomething
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        t.Id,
        t.Price,
        c.CustomerName
    FROM dbo.TableName1 t1
    INNER JOIN dbo.TableName2 t2
        ON t1.CustomerId = t2.CustomerId
    // WHERE cluase also included
END;
GO
```

### Insert Procedure Generation

Generate INSERT procedures automatically.

Example:

```sql
CREATE PROCEDURE dbo.InsertTableName
(
    @Price DECIMAL(18,2),
    @Quantity INT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.TableName
    (
        Price,
        Quantity
    )
    VALUES
    (
        @Price,
        @Quantity
    );
END;
GO
```

---

## Technology Stack

- .NET 9
- ASP.NET Core Web API
- Swagger/OpenAPI
- Dependency Injection
- SQL Server

---

## Project Structure

```text
SqlAssistant
│
├── Controllers
│   └── ProcedureController.cs
│
├── Models
│   ├── Requests
│   ├── Responses
│   └── Metadata
│
├── Services
│   ├── Generators
│   │   └── SPGeneratorService.cs
│   ├── Metadata
│   └── OpenAI
│
├── Program.cs
│
└── README.md
```

---

## API Endpoint

### Generate Stored Procedure

```http
POST /api/procedure/generate
```

### Request Example

```json
{
  "schema": "dbo",
  "database": "DBName",
  "baseTable": "TableName",
  "type": 0,
  "name": "GetAll[Something]",
  "requiredColumns": [
    "Id",
    "Price",
    "Quantity"
  ],
  "joins": [
    {
      "joinType": "INNER",
      "leftTable": "Table1",
      "leftColumn": "Id",
      "rightTable": "Table2",
      "rightColumn": "T2Id"
    }
  ],
  "filters": [
    {
      "column": "T2Id",
      "operator": "=",
      "value": "@T2Id"
    }
  ]
}
```

### Response

```json
{
  "generateSql": "CREATE PROCEDURE ...",
  "isValid": true,
  "validationErrors": null
}
```

---

## Running Locally

Clone repository:

```bash
git clone <repository-url>
```

Navigate to project:

```bash
cd SqlAssistant
```

Restore packages:

```bash
dotnet restore
```

Run application:

```bash
dotnet run
```

Open Swagger:

```text
https://localhost:{port}/swagger
```

---

## Supported Procedure Types

| Type | Description |
|--------|-------------|
| 0 | Select |
| 1 | Insert |
| 2 | Update (Planned) |
| 3 | Delete (Planned) |
| 4 | Merge / Upsert (Planned) |

---

## Roadmap

### Phase 1
- [x] Select generation
- [x] Join generation
- [x] Dynamic filters
- [x] Insert generation
- [x] Swagger integration

### Phase 2
- [ ] Update generation
- [ ] Delete generation
- [ ] Merge generation
- [ ] Audit field support
- [ ] Parameter generation

### Phase 3
- [ ] Database metadata discovery
- [ ] Automatic join detection
- [ ] SQL validation engine
- [ ] SQL formatting engine
- [ ] AI-assisted procedure generation

### Phase 4
- [ ] React Frontend
- [ ] SQL preview window
- [ ] Export to .sql file
- [ ] Release Notes generation
- [ ] CI/CD integration

---

## Coding Standards

Generated procedures follow:

- ANSI_NULLS ON
- QUOTED_IDENTIFIER ON
- SET NOCOUNT ON
- Schema-qualified object names
- Consistent formatting
- Explicit column lists

---

## Future Vision

SQL Assistant aims to become an intelligent SQL development platform capable of:

- Generating enterprise-grade stored procedures
- Discovering database metadata automatically
- Recommending joins and relationships
- Producing release notes and change logs
- Integrating with AI models for SQL generation and validation

---

## Author

Michael Gobena

Built as a learning and productivity project to automate SQL Server stored procedure development and improve consistency across database teams.
