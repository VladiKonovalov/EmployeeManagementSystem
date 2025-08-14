# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore EmployeeManagementSystem.sln
RUN dotnet publish EMS.Web/EMS.Web.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
RUN mkdir -p /app/App_Data /app/Logs
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EMS.Web.dll"]
