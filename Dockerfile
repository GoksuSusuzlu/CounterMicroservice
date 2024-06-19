# Use the official .NET 8.0 SDK image as the build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy the project files and restore dependencies
COPY src/counterMicroservice/WebAPI/WebAPI.csproj ./src/counterMicroservice/WebAPI/
COPY src/counterMicroservice/Application/Application.csproj ./src/counterMicroservice/Application/
COPY src/counterMicroservice/Domain/Domain.csproj ./src/counterMicroservice/Domain/
COPY src/counterMicroservice/Infrastructure/Infrastructure.csproj ./src/counterMicroservice/Infrastructure/
RUN dotnet restore ./src/counterMicroservice/WebAPI/WebAPI.csproj

# Copy the rest of the application code
COPY . .

# Build the application
RUN dotnet publish ./src/counterMicroservice/WebAPI/WebAPI.csproj -c Release -o /out

# Use the official .NET 8.0 runtime image as the base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Expose the port the app runs on
EXPOSE 80

# SQL Server trusted connection problem
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf

# Default entrypoint for the web application
ENTRYPOINT ["dotnet", "WebAPI.dll"]
