# Library Management System

## How to Run the Application

1. Clone the repository:
   git clone https://github.com/IbizaSummer/LibraryManagement
   cd LibraryManagement

2. Install dependencies:
   dotnet restore

3. Run migrations to set up the database:

   dotnet ef migrations add InitialCreate
   
   dotnet ef database update

5. Run the application:
   dotnet run

6. Open the browser:
   http://localhost:5000

## Setting up the Database

- Make sure your connection string is set in `appsettings.json`:

  "ConnectionStrings": {
    "DefaultConnection": "Data Source=library.db"
  }

## Known Issue

If `GetAuthorById(int id)` returns null, an error message will be shown.
