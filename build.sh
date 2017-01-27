#!/usr/bin/env bash
dotnet restore 
dotnet build **/project.json
dotnet test ./src/StarterProject.UnitTests/project.json
dotnet test ./src/StarterProject.InMemoryUnitTests/project.json
dotnet test ./src/StarterProject.IntegrationTests/project.json