#!/usr/bin/env bash
dotnet restore 
dotnet build **/project.json
dotnet test ./src/StarterProject.UnitTests/project.json