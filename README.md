# Vehicle Condition Tracker (Backend)

Clean Architecture–style .NET 10 Web API for capturing vehicle condition reports, sections, findings, photos, and generating PDFs.

## Project layout
```
VehicleConditionTracker.sln
src/
  VehicleConditionTracker.Api/            // Presentation (controllers, middleware, DI wiring)
  VehicleConditionTracker.Application/    // DTOs, interfaces, use-case wiring
  VehicleConditionTracker.Domain/         // Entities and enums
  VehicleConditionTracker.Infrastructure/ // EF Core, auth, storage, PDF
```

## Prerequisites
- .NET 10 SDK
- PostgreSQL running locally (default connection in appsettings)
- (Optional) `dotnet-ef` tool if you add migrations later

## Quick start
```bash
# from repo root
dotnet restore VehicleConditionTracker.sln
dotnet build VehicleConditionTracker.sln
dotnet run --project src/VehicleConditionTracker.Api/VehicleConditionTracker.Api.csproj
```

API will listen on the standard Kestrel ports; Swagger UI available at `/swagger`.

## Configuration
- `src/VehicleConditionTracker.Api/appsettings.json` and `appsettings.Development.json` hold:
  - `JwtSettings` (Issuer, Audience, SecretKey, AccessTokenMinutes)
  - `ConnectionStrings:DefaultConnection` (PostgreSQL)
  - `FileStorage:RootPath` (local uploads folder, also exposed at `/uploads`)
- Replace placeholder secrets before production use.

## Authentication
- JWT bearer (stub): token generation is implemented; credential validation and user persistence are TODO.

## Storage & PDFs
- File storage: local filesystem via `LocalFileStorage` (swappable later).
- PDF: `QuestPdfService` stubbed; implement rendering when report template is ready.

## Domain model (core)
- Entities: `User`, `VehicleReport`, `VehicleSection`, `VehicleFinding`, `VehiclePhoto`
- Enums: `ReportStatus`, `SectionType`, `FindingSeverity`

## Current endpoints (scaffolded)
- `POST /api/auth/register`
- `POST /api/auth/login`
- `GET /api/reports`
- `GET /api/reports/{id}`
- `POST /api/reports`
- `PUT /api/reports/{id}`
- `PATCH /api/reports/{id}/status`
- `DELETE /api/reports/{id}`
- Section/finding/photo endpoints under `/api` (see controllers)
- `GET /api/reports/{id}/pdf` (stub)

## Building per project
```bash
dotnet build src/VehicleConditionTracker.Domain/VehicleConditionTracker.Domain.csproj
dotnet build src/VehicleConditionTracker.Application/VehicleConditionTracker.Application.csproj
dotnet build src/VehicleConditionTracker.Infrastructure/VehicleConditionTracker.Infrastructure.csproj
dotnet build src/VehicleConditionTracker.Api/VehicleConditionTracker.Api.csproj
```
