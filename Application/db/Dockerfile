FROM mcr.microsoft.com/mssql/server:2022-latest

# Set environment variables
# ENV MSSQL_SA_PASSWORD=YourStrongPassword123! ACCEPT_EULA=Y

# Switch to root user to copy the script
USER root

# Copy the SQL script into the container
COPY script.sql /docker-entrypoint-initdb.d/script.sql

# Switch back to mssql user
USER mssql