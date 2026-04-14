# DPED Student Application Tracking System

Blazor Server application for Diploma in Physical Education admission workflow with:

- ASP.NET Core Identity login and registration
- SQL Server with Entity Framework Core
- Student application form and academic details
- Dynamic fee calculation and simulated payment
- Registration number generation
- Printable application view
- Admin dashboard with filtering and CSV export

## Default Admin Login

- Email: `admin@dped.local`
- Password: `Admin123`

## Run Steps

1. Install .NET 8 SDK and SQL Server or LocalDB.
2. Update `appsettings.json` if needed.
3. Run `dotnet restore`
4. Run `dotnet run`

The app uses `Database.EnsureCreated()` during startup, so it will create the database schema automatically on first run.
