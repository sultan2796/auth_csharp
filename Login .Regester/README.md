# Login.Regester - Production Deployment Guide

## 1. Build and Publish

```sh
dotnet publish -c Release -o ./publish
```

## 2. Copy Files to Server
- Copy the contents of the `./publish` folder to your production server.
- If using SQLite, also copy your `users.db` file or let the app create a new one.

## 3. Configure Environment
- Set environment variable for production:
  - **Windows:**
    ```sh
    set ASPNETCORE_ENVIRONMENT=Production
    ```
  - **Linux/macOS:**
    ```sh
    export ASPNETCORE_ENVIRONMENT=Production
    ```
- (Optional) Override connection string and JWT key with environment variables:
  - **Windows:**
    ```sh
    set ConnectionStrings__DefaultConnection=Data Source=/var/app/users.db
    set Jwt__Key=your_production_secret
    ```
  - **Linux/macOS:**
    ```sh
    export ConnectionStrings__DefaultConnection=Data Source=/var/app/users.db
    export Jwt__Key=your_production_secret
    ```

## 4. Run the Application
```sh
dotnet Login .Regester.dll
```

## 5. Access the API
- By default, the app will listen on the configured port (see `appsettings.Production.json` or environment variables).
- Swagger UI will be available if enabled in production.

## 6. Notes
- **Security:** Always use a strong, unique JWT key in production.
- **Database:** For multi-user or scalable scenarios, consider using a server-based DB (SQL Server, PostgreSQL, etc.) instead of SQLite.
- **Reverse Proxy:** For public deployments, use a reverse proxy (Nginx, Apache, IIS) for SSL termination and security.

---

**Contact:** For further help, check .NET documentation or contact your DevOps team.

