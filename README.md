Project Setup and Usage Guide
```env

Setup Instructions

Environment Configuration

Create a .env file in the root directory with the following keys:


DB_CONNECTION_STRING=<your db connection string> 
AWS_STORAGE_BUCKET_NAME=<aws bucket name>
AWS_ACCESS_KEY_ID=<your AWS access key>
AWS_SECRET_ACCESS_KEY=<your AWS secret key>
DEFAULT_IMAGE=<default pre-uploaded image path>
DB_CONNECTION_STRING=Server=localhost;Database=TbcDb;Trusted_Connection=True;TrustServerCertificate=true;


Database Management

Add Migrations

Run the following command to add a migration:

dotnet ef migrations add <migrationName> --project Infrastructure --startup-project Web.api

Update Database

Apply the migrations with:

dotnet ef database update --project Infrastructure --startup-project Web.api

Before adding a user, you should upload the image to the bucket using a pre-signed URL, and then include the given URL in the AddUser request 
