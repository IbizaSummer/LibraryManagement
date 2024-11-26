
# Swagger Documentation

This project implements a comprehensive REST API for a Library Management System. The API includes endpoints for managing books, authors, customers, and library branches. All endpoints use JSON for both requests and responses, and the documentation is available via Swagger.

### Accessing Swagger UI

The Swagger documentation can be accessed at:

- **URL**: `https://localhost:<port>/swagger`
- **Features**:
  - Explore all API endpoints.
  - View detailed descriptions of endpoint functionalities.
  - Understand JSON payload structures for requests and responses.
  - Test endpoints directly from the Swagger UI.

### API Endpoints

#### 1. Book Management
- **GET /api/BookApi**: Retrieve all books.
- **GET /api/BookApi/{id}**: Retrieve a specific book by ID.
- **POST /api/BookApi**: Create a new book.
- **PUT /api/BookApi/{id}**: Update an existing book.
- **DELETE /api/BookApi/{id}**: Delete a book.

#### 2. Author Management
- **GET /api/AuthorApi**: Retrieve all authors.
- **GET /api/AuthorApi/{id}**: Retrieve a specific author by ID.
- **POST /api/AuthorApi**: Create a new author.
- **PUT /api/AuthorApi/{id}**: Update an existing author.
- **DELETE /api/AuthorApi/{id}**: Delete an author.

#### 3. Customer Management
- **GET /api/CustomerApi**: Retrieve all customers.
- **GET /api/CustomerApi/{id}**: Retrieve a specific customer by ID.
- **POST /api/CustomerApi**: Create a new customer.
- **PUT /api/CustomerApi/{id}**: Update an existing customer.
- **DELETE /api/CustomerApi/{id}**: Delete a customer.

#### 4. Library Branch Management
- **GET /api/LibraryBranchApi**: Retrieve all library branches.
- **GET /api/LibraryBranchApi/{id}**: Retrieve a specific library branch by ID.
- **POST /api/LibraryBranchApi**: Create a new library branch.
- **PUT /api/LibraryBranchApi/{id}**: Update an existing library branch.
- **DELETE /api/LibraryBranchApi/{id}**: Delete a library branch.
