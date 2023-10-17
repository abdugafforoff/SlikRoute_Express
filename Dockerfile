# Use the official .NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BIS_project/BIS_project.csproj", "BIS_project/"]
RUN dotnet restore "YourApp/YourApp.csproj"
COPY . .
WORKDIR "/src/YourApp"
RUN dotnet build "BIS_project.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BIS_project.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BIS_project.dll"]
