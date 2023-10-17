# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET SDK as a build image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BIS_project/BIS_project.csproj", "BIS_project/"]
RUN dotnet restore "BIS_project/BIS_project.csproj"
COPY . .
WORKDIR "/src/BIS_project"
RUN dotnet publish "BIS_project.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BIS_project.dll"]
