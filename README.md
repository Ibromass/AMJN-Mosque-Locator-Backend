# AMJN Mosque Locator

AMJN Mosque Locator is an ASP.NET Core Web API for managing and locating Ahmadiyya mosques. It provides public endpoints for browsing mosque records and nearby mosque searches, plus JWT-protected admin endpoints for creating, updating, and deactivating mosque entries.

## Features

- List all registered mosques
- Get a mosque by ID
- Search for nearby mosques by latitude, longitude, and radius
- Admin login with JWT authentication
- Create, update, and deactivate mosque records
- PostgreSQL persistence with Entity Framework Core migrations
- Swagger UI for API exploration
- Docker support for deployment

## Tech Stack

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- JWT Bearer authentication
- Swagger / OpenAPI
- Docker

## Project Structure

```text
AMJN-Mosque-Locator/
├── AMJN-Mosque-Locator.sln
├── AMJN-Mosque-Locator/
│   ├── Controllers/
│   ├── DTOs/
│   ├── Entities/
│   ├── Implementations/
│   ├── Interfaces/
│   ├── Migrations/
│   ├── Program.cs
│   ├── appsettings.json
│   └── Dockerfile
└── AMJN-Mosque-Locator.Tests/
```

## Requirements

- .NET SDK 9.0 or later
- PostgreSQL database
- Docker, optional

## Configuration

The application expects a PostgreSQL connection string and JWT settings.

For local development, configure these values using user secrets, environment variables, or a local-only settings file:

```json
{
  "ConnectionStrings": {
    "AMJNDbContext": "Host=localhost;Port=5432;Database=amjn_db;Username=postgres;Password=your_password"
  },
  "JwtTokenSettings": {
    "TokenKey": "your-secure-token-key",
    "TokenIssuer": "AMJN_Mosque_Locator",
    "Audience": "AMJN_Mosque_Locator",
    "TokenExpiryPeriod": "60"
  }
}
```

> Do not commit production database credentials or JWT secrets to source control.

## Getting Started

Clone the repository:

```bash
git clone <repository-url>
cd AMJN-Mosque-Locator
```

Restore dependencies:

```bash
dotnet restore
```

Apply database migrations:

```bash
dotnet ef database update --project AMJN-Mosque-Locator
```

Run the API:

```bash
dotnet run --project AMJN-Mosque-Locator
```

Open Swagger UI:

```text
http://localhost:5191
```

## API Endpoints

### Authentication

| Method | Endpoint | Description |
| --- | --- | --- |
| POST | `/api/Adminauth/login` | Log in as an admin and receive a JWT |

### Mosques

| Method | Endpoint | Auth | Description |
| --- | --- | --- | --- |
| GET | `/api/Mosque` | Public | Get all mosques |
| GET | `/api/Mosque/{id}` | Public | Get a mosque by ID |
| GET | `/api/Mosque/nearby` | Public | Search nearby mosques |
| POST | `/api/Mosque` | JWT required | Create a mosque |
| PUT | `/api/Mosque/{id}` | JWT required | Update a mosque |
| DELETE | `/api/Mosque/{id}` | JWT required | Deactivate a mosque |

Protected endpoints require an authorization header:

```text
Authorization: Bearer <jwt-token>
```

## Docker

Build the Docker image from the project directory:

```bash
cd AMJN-Mosque-Locator
docker build -t amjn-mosque-locator .
```

Run the container:

```bash
docker run -p 10000:10000 \
  -e PORT=10000 \
  -e ConnectionStrings__AMJNDbContext="Host=host.docker.internal;Port=5432;Database=amjn_db;Username=postgres;Password=your_password" \
  -e JwtTokenSettings__TokenKey="your-secure-token-key" \
  amjn-mosque-locator
```

Then open:

```text
http://localhost:10000
```

## Database Migrations

Add a new migration:

```bash
dotnet ef migrations add <MigrationName> --project AMJN-Mosque-Locator
```

Update the database:

```bash
dotnet ef database update --project AMJN-Mosque-Locator
```

The application also attempts to apply pending migrations automatically on startup.

## Testing

Run tests with:

```bash
dotnet test
```

## Notes

- Swagger is enabled and served from the application root.
- CORS currently allows any origin, header, and method.
- The Dockerfile is configured to listen on the `PORT` environment variable, defaulting to `10000`.
