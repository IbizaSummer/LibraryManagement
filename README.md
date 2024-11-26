
# Library Management System API

This project implements a RESTful API for managing a library system, including books, authors, customers, and library branches. It uses ASP.NET Core for backend services and SQLite for data persistence.

## Features

1. **RESTful Endpoints**:
   - Create, retrieve, update, and delete records for books, authors, customers, and library branches.
2. **JSON Support**:
   - All API interactions use JSON for request and response payloads.
3. **Swagger Documentation**:
   - API documentation and testing are available through Swagger UI.
4. **Database Integration**:
   - Persistent storage using SQLite with EF Core.
5. **Authentication**:
   - Supports user authentication via ASP.NET Identity with optional Google and Facebook login.

## Installation

### Prerequisites
- .NET 6.0 SDK or later
- SQLite

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/IbizaSummer/LibraryManagement
   cd LibraryManagement
   ```

2. Install dependencies:
   ```bash
   dotnet restore
   ```

3. Update the database:
   ```bash
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. Open the application in your browser:
   ```
   https://localhost:<port>
   ```

## Using the API

### Base URL
The base URL for the API is:
```
https://localhost:<port>/api
```

### JSON Payloads
All requests and responses use JSON format. See Swagger documentation for detailed examples.

## Testing the API
You can test the API using:
- **Swagger UI**: Navigate to `https://localhost:<port>/swagger`
- **Postman**: Import endpoints manually or export from Swagger.

## Contributing

1. Fork the repository.
2. Create a new branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Description of changes"
   ```
4. Push to the branch:
   ```bash
   git push origin feature/your-feature-name
   ```
5. Create a pull request.

## License
This project is licensed under the MIT License. See the LICENSE file for details.
