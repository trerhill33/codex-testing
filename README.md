# Clean Architecture Sample

This repository contains a small .NET Clean Architecture sample focused on a todo feature. The solution is structured into four projects:

- **Domain**: Contains core domain entities and logic.
- **Application**: Holds application services and repository interfaces.
- **Infrastructure**: Provides infrastructure implementations such as the in-memory repository.
- **WebApi**: ASP.NET Core Web API that wires up dependencies and exposes REST endpoints.

To build or run the project you will need the .NET 8 SDK installed locally:

```bash
dotnet restore
dotnet build
dotnet run --project src/WebApi/WebApi.csproj
```

The API exposes a simple todo controller under `/api/todo` with endpoints to list, create, complete, and delete todo items.
