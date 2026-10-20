# TodoApi

A RESTful Todo API built with **.NET 9* following **Clean Architecture** principles. It provides secure, user-scoped task management with JWT authentication, input validation, pagination, and filtering.

## Features

- JWT-based authentication and authorization
- Full CRUD operations for tasks, scoped to the authenticated user
- Request validation with FluentValidation
- Pagination and filtering on list endpoints
- Data access with Entity Framework Core and PostgreSQL
- Clean Architecture with clear separation of concerns

## Tech Stack

| Area            | Technology                  |
| --------------- | --------------------------- |
| Framework       | .NET 8 / ASP.NET Core       |
| Language        | C#                          |
| ORM             | Entity Framework Core       |
| Database        | PostgreSQL                  |
| Authentication  | JWT Bearer                  |
| Validation      | FluentValidation            |

## Architecture

The solution follows Clean Architecture, where dependencies point inward toward the domain:

```
TodoApi/
├── src/
│   ├── TodoApi.Domain/           # Entities and core business rules
│   ├── TodoApi.Application/      # Use cases, DTOs, validators, interfaces
│   ├── TodoApi.Infrastructure/   # EF Core, repositories, auth services
│   └── TodoApi.API/              # Controllers, middleware, configuration
└── README.md
```

> Adjust the folder names above to match your actual solution structure.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)

### Installation

1. Clone the repository:

```bash
   git clone https://github.com/[your-username]/TodoApi.git
   cd TodoApi
```

2. Configure the connection string and JWT settings (see [Configuration](#configuration)).

3. Apply the database migrations:

```bash
   dotnet ef database update --project src/TodoApi.Infrastructure --startup-project src/TodoApi.API
```

4. Run the API:

```bash
   dotnet run --project src/TodoApi.API
```

## Configuration

Create an `appsettings.Development.json` file (or use [user secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets)) with the following structure:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=todoapi;Username=[user];Password=[password]"
  },
  "Jwt": {
    "Key": "[your-secret-key-min-32-characters]",
    "Issuer": "TodoApi",
    "Audience": "TodoApiClient",
    "ExpiresInMinutes": 60
  }
}
```

> Never commit real credentials or secret keys to the repository.

## API Endpoints

### Authentication

| Method | Endpoint             | Description                  |
| ------ | -------------------- | ---------------------------- |
| POST   | `/api/auth/register` | Register a new user          |
| POST   | `/api/auth/login`    | Authenticate and get a token |

### Tasks (requires `Authorization: Bearer <token>`)

| Method | Endpoint           | Description                              |
| ------ | ------------------ | ---------------------------------------- |
| GET    | `/api/tasks`       | List tasks (supports pagination/filters) |
| GET    | `/api/tasks/{id}`  | Get a task by its ID                     |
| POST   | `/api/tasks`       | Create a new task                        |
| PUT    | `/api/tasks/{id}`  | Update an existing task                  |
| DELETE | `/api/tasks/{id}`  | Delete a task                            |

### Query Parameters (list endpoint)

| Parameter    | Description                    | Example            |
| ------------ | ------------------------------ | ------------------ |
| `pageNumber` | Page to retrieve               | `?pageNumber=1`    |
| `pageSize`   | Number of items per page       | `&pageSize=10`     |
| `isCompleted`| Filter by completion status    | `&isCompleted=true`|

> Update the routes and parameters to match your controllers.

## What I Learned

- Structuring a .NET solution with Clean Architecture
- Implementing stateless authentication with JWT
- Validating requests with FluentValidation
- Working with EF Core migrations and relational databases

## Roadmap

- [ ] Unit and integration tests
- [ ] Docker support
- [ ] Swagger/OpenAPI documentation improvements
- [ ] CI pipeline with GitHub Actions

## Author

**Maicol** — Software Engineering student and backend developer  
[GitHub](https://github.com/MikeAbstract) · [LinkedIn](www.linkedin.com/in/maicol-alcantara-b6102535b)

## License

This project is licensed under the MIT License.