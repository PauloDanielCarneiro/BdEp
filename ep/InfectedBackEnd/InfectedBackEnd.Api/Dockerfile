# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . .
RUN ls -l
RUN dotnet restore "InfectedBackEnd.Api/InfectedBackEnd.Api.csproj"

# Copy everything else and build
RUN dotnet publish "InfectedBackEnd.Api/InfectedBackEnd.Api.csproj" -c Debug -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "InfectedBackEnd.Api.dll"]
