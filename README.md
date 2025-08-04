Project Setup and Usage Guide
```env

Setup Instructions

Environment Configuration

Create a .env file in the root directory with the following keys:


DB_CONNECTION_STRING=<your db connection string>
DB_CONNECTION_STRING=Server=localhost;Database=TbcDb;Trusted_Connection=True;TrustServerCertificate=true;


Database Management

Add Migrations

Run the following command to add a migration:

dotnet ef migrations add <migrationName> --project Infrastructure --startup-project Web.api

Update Database

Apply the migrations with:

dotnet ef database update --project Infrastructure --startup-project Web.api

