# Snackis

A forum app built with .NET. Blazor Server frontend, ASP.NET Core Web API backend, deployed on Azure.

## Stack

- **Frontend** – Blazor Server
- **Backend** – ASP.NET Core Web API
- **Database** – Azure SQL (EF Core)
- **Storage** – Azure Blob Storage (images)
- **Auth** – ASP.NET Core Identity

## Structure

Clean/onion architecture split across five projects:

```
Snackis.Domain          # Entities & interfaces
Snackis.Application     # Services & DTOs
Snackis.Infrastructure  # EF Core, repositories
Snackis.API             # Controllers
Snackis.Presentation    # Blazor frontend (also hosts the API)
```

## Features

- Categories → Subcategories → Topics → Posts
- Private messages (inbox + sent)
- Image uploads via Azure Blob Storage
- Content reporting
- Cookie-based authentication

## Running locally

1. Add `appsettings.json` to `Snackis.Presentation` with your connection strings
2. `dotnet ef database update --project Snackis.Infrastructure --startup-project Snackis.Presentation`
3. `dotnet run --project Snackis.Presentation`
