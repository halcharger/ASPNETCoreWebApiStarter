#!/usr/bin/env bash
dotnet restore 
dotnet build ./src/StarterProject.UnitTests/StarterProject.UnitTests.csproj
dotnet build ./src/StarterProject.InMemoryUnitTests/StarterProject.InMemoryUnitTests.csproj
dotnet build ./src/StarterProject.IntegrationTests/StarterProject.IntegrationTests.csproj
dotnet build ./src/StarterProject.WebApi/StarterProject.WebApi.csproj
dotnet test ./src/StarterProject.UnitTests/StarterProject.UnitTests.csproj
dotnet test ./src/StarterProject.InMemoryUnitTests/StarterProject.InMemoryUnitTests.csproj
dotnet test ./src/StarterProject.IntegrationTests/StarterProject.IntegrationTests.csproj