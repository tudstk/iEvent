# Use .NET SDK image for building the backend
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set working directory
WORKDIR /IEvent

# # Copy solution file
COPY *.sln ./

# # Copy all project files
COPY **/*.csproj ./

# Copy the rest of the application and build
COPY ./* .

# Restore the solution
RUN dotnet restore

# Publish the application
RUN dotnet publish -c Release -o out

# Use ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /IEvent/out .

# Expose the application's port
EXPOSE 5000

CMD ["dotnet", "IEvent.API.dll"]
