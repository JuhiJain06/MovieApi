# MovieAPI

A REST API for managing movies, actors, producers, genres and reviews, built with ASP.NET Core (.NET 5), Dapper and SQL Server. It includes JWT authentication and stores movie posters in Supabase Storage.

## Features

- CRUD endpoints for Movies, Actors, Producers, Genres and Reviews
- Sign up and login with hashed passwords and JWT bearer tokens
- Movie poster upload to Supabase Storage
- Layered architecture: Controllers → Services → Repositories
- Global exception-handling middleware with custom exceptions
- Swagger UI for exploring the API

## Tech stack

ASP.NET Core 5 · Dapper · SQL Server · JWT · Supabase · Swagger

## Running locally

1. Clone the repo and open `MovieAPI.slnx` in Visual Studio.
2. Create a SQL Server database called `MovieAPI`.
3. Set your secrets (don't put them in `appsettings.json`):

```bash
cd MovieAPI
dotnet user-secrets init
dotnet user-secrets set "JWT:Secret" "a-long-random-string-at-least-32-characters"
dotnet user-secrets set "Supabase:Url" "https://your-project.supabase.co"
dotnet user-secrets set "Supabase:Key" "your-supabase-key"
```

4. Run the project and open `/swagger`.
