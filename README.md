# Library Management System

## Introduction
This is a Library Management System application built using ASP.NET Core. The application features user authentication (Login, Logout, Register), and social media authentication using Google and Facebook.

## Prerequisites
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- Visual Studio 2022 or any code editor with .NET support (e.g., VS Code)
- A configured Google and Facebook app for social media authentication
- SQLite for database storage

## Steps to Run the Application

### 1. Clone the Repository
```bash
git clone https://github.com/IbizaSummer/LibraryManagement
cd LibraryManagement
```

### 2. Configure User-Secrets
Set up the client IDs and secrets for Google and Facebook authentication:
```bash
dotnet user-secrets set "Authentication:Google:ClientId" "<your-google-client-id>"
dotnet user-secrets set "Authentication:Google:ClientSecret" "<your-google-client-secret>"
dotnet user-secrets set "Authentication:Facebook:ClientId" "<your-facebook-client-id>"
dotnet user-secrets set "Authentication:Facebook:ClientSecret" "<your-facebook-client-secret>"
```

### 3. Update the Database
Run migrations to set up the SQLite database:
```bash
dotnet ef database update
```

### 4. Run the Application
Start the application using the following command:
```bash
dotnet run
```
The application will be available at `https://localhost:5001` (or a similar URL displayed in the console).

### 5. Configure Redirect URIs
Ensure the redirect URIs in your Google and Facebook developer console match the application's callback URLs:
- Google: `https://localhost:5001/signin-google`
- Facebook: `https://localhost:5001/signin-facebook`

### 6. Test Social Media Authentication
1. Navigate to the Login page.
2. Click on "Login with Google" or "Login with Facebook".
3. Use valid credentials to authenticate through the respective social media platform.
4. Verify the user is successfully logged in and their details are stored in the database.

## Acknowledgements
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Google Developers Console](https://console.developers.google.com/)
- [Meta for Developers](https://developers.facebook.com/)

## License
This project is licensed under the MIT License.
