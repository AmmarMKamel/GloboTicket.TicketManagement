# GloboTicket.TicketManagement

A modern, full-stack event ticket management system built with ASP.NET Core and Blazor WebAssembly. This project demonstrates clean architecture principles with a well-structured layered approach.

## Table of Contents

- [Project Overview](#project-overview)
- [Architecture](#architecture)
- [Technologies & Dependencies](#technologies--dependencies)
- [Prerequisites](#prerequisites)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
- [Testing](#testing)
- [Features](#features)
- [Development](#development)

## Project Overview

GloboTicket.TicketManagement is a ticket management platform for event organizers and customers. It provides:

- **Event Management**: Create and manage events and categories
- **Ticket Ordering**: Browse and order tickets for events
- **User Authentication**: Secure user authentication and authorization
- **Admin Dashboard**: Administrative interface for managing events and categories
- **RESTful API**: Complete API for backend operations

## Architecture

The project follows **Clean Architecture** principles with a layered structure:

```
├── GloboTicket.TicketManagement.Domain           # Domain entities and business rules
├── GloboTicket.TicketManagement.Application      # Business logic and use cases
├── GloboTicket.TicketManagement.Infrastructure   # External services (email, file export, etc.)
├── GloboTicket.TicketManagement.Persistence      # Data access layer (EF Core, repositories)
├── GloboTicket.TicketManagement.Identity         # Identity and authentication
├── GloboTicket.TicketManagement.Api              # ASP.NET Core REST API
├── GloboTicket.TicketManagement.App              # Blazor WebAssembly frontend
├── GloboTicket.TicketManagement.Api.IntegrationTests        # API integration tests
├── GloboTicket.TicketManagement.Persistence.IntegrationTests # Database integration tests
└── GloboTicket.TicketManagement.Application.UnitTests       # Unit tests
```

### Layer Responsibilities

- **Domain**: Core business entities (Event, Category, Order, etc.) and business logic
- **Application**: Use cases, business workflows, DTOs, and application services
- **Infrastructure**: External service implementations (email, file export)
- **Persistence**: Entity Framework Core configuration, migrations, and repositories
- **Identity**: ASP.NET Core Identity setup and authentication configuration
- **API**: REST API controllers, middleware, and request/response handling
- **App**: Blazor WebAssembly UI with components and pages

## Technologies & Dependencies

### Core Framework

- **.NET 10.0** - Latest .NET framework
- **ASP.NET Core** - Backend web framework
- **Blazor WebAssembly** - Frontend SPA framework

### Data & ORM

- **Entity Framework Core 10.0.7** - ORM
- **SQL Server** - Database (configured in migrations)

### Logging & Diagnostics

- **Serilog 4.3.1** - Structured logging
- **Serilog.AspNetCore 10.0.0** - ASP.NET Core integration
- **Serilog.Sinks.File 7.0.0** - File logging sink

### API & Documentation

- **Swashbuckle.AspNetCore 10.1.7** - Swagger/OpenAPI documentation

### Frontend

- **Blazored.LocalStorage** - Browser local storage access
- **AutoMapper** - Object mapping

### Testing

- **xUnit** or similar (configured in test projects)

## Prerequisites

- **.NET 10.0 SDK** or later
- **Visual Studio 2022** (Community, Professional, or Enterprise) or **Visual Studio Code** with C# Dev Kit
- **SQL Server** (LocalDB or Express)

## Project Structure

### Controllers & API Endpoints

**API Project** (`GloboTicket.TicketManagement.Api`):

- `CategoryController.cs` - Category management endpoints
- `EventsController.cs` - Event management endpoints
- `OrderController.cs` - Order management endpoints

### Business Logic

**Application Project** (`GloboTicket.TicketManagement.Application`):

- `Features/` - Use case implementations (queries and commands)
- `Models/` - DTOs and view models
- `Profiles/` - AutoMapper profiles for entity-to-DTO mapping
- `Contracts/` - Interface definitions

### Database

**Persistence Project** (`GloboTicket.TicketManagement.Persistence`):

- `Configurations/` - EF Core entity configurations
- `Migrations/` - Database migrations
- `Repositories/` - Repository implementations

### Identity

**Identity Project** (`GloboTicket.TicketManagement.Identity`):

- ASP.NET Core Identity setup
- User roles and claims configuration

## Getting Started

### 1. Clone the Project

```bash
git clone https://github.com/AmmarMKamel/GloboTicket.TicketManagement.git
cd GloboTicket.TicketManagement
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Configure Database

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "GloboTicketDbConnection": "Server=.;Database=GloboTicketDb;Trusted_Connection=true;"
  }
}
```

### 4. Apply Migrations

```bash
dotnet ef database update --project GloboTicket.TicketManagement.Persistence
```

## Running the Application

### Option 1: Run Both API and Frontend Together

```bash
dotnet run --project GloboTicket.TicketManagement.Api
```

The API will start at `https://localhost:7081`

In a separate terminal:

```bash
dotnet run --project GloboTicket.TicketManagement.App
```

The frontend will start at `https://localhost:7220` (or similar)

### Option 2: Run via Visual Studio

1. Open the solution file: `GloboTicket.TicketManagement.slnx`
2. Set multiple startup projects:
   - `GloboTicket.TicketManagement.Api`
   - `GloboTicket.TicketManagement.App`
3. Press `F5` to start debugging

### Option 3: Run via Visual Studio Code

1. Open the workspace folder
2. Use the integrated terminal to run:
   ```bash
   dotnet run --project GloboTicket.TicketManagement.Api
   ```

## API Endpoints

### Categories

- `GET /api/categories/all` - Get all categories
- `GET /api/categories/allwithevents?includeHistory={bool}` - Get categories with their events
- `POST /api/categories` - Create new category

### Events

- `GET /api/events` - Get all events
- `GET /api/events/{id}` - Get event by ID
- `POST /api/events` - Create new event
- `PUT /api/events` - Update event
- `DELETE /api/events/{id}` - Delete event
- `GET /api/events/export` - Export events as CSV file

### Orders

- _(Currently under development - no endpoints implemented yet)_

### Swagger Documentation

Access API documentation at: `https://localhost:7081/swagger`

## Testing

### Run All Tests

```bash
dotnet test
```

### Run Specific Test Project

```bash
dotnet test GloboTicket.TicketManagement.Application.UnitTests
dotnet test GloboTicket.TicketManagement.Persistence.IntegrationTests
dotnet test GloboTicket.TicketManagement.Api.IntegrationTests
```

### Test Projects

- **Application.UnitTests**: Unit tests for business logic and application services
- **Persistence.IntegrationTests**: Integration tests for database operations
- **Api.IntegrationTests**: Integration tests for API endpoints

## Features

- ✅ Event and category management
- ✅ Ticket ordering system
- ✅ User authentication and authorization
- ✅ RESTful API with Swagger documentation
- ✅ Structured logging with Serilog
- ✅ Entity Framework Core with migrations
- ✅ Clean architecture separation of concerns
- ✅ Comprehensive test coverage
- ✅ Blazor WebAssembly UI
- ✅ AutoMapper for entity mapping

## Development

### Code Style & Standards

- Follow C# naming conventions (PascalCase for public members, camelCase for private)
- Use async/await patterns consistently
- Apply dependency injection for testability
- Write self-documenting code with meaningful names

### Adding a New Feature

1. Create domain entity in `Domain` project
2. Add EF Core configuration in `Persistence` project
3. Create DTOs in `Application` project
4. Implement feature handler in `Application/Features/`
5. Add controller endpoints in `Api` project
6. Create UI components in `App` project
7. Write tests in appropriate test projects

### Database Migrations

Create a new migration:

```bash
dotnet ef migrations add [MigrationName] --project GloboTicket.TicketManagement.Persistence
```

Apply migrations:

```bash
dotnet ef database update --project GloboTicket.TicketManagement.Persistence
```

### Logging

Serilog is configured in `Program.cs`. Logs are written to:

- Console output
- File sink (check `appsettings.json` for path)

## License

This project is for learning and educational purposes.
