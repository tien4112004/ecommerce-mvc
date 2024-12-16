# EcommerceMVC

EcommerceMVC is a robust ASP.NET Core MVC application designed to manage an e-commerce platform efficiently. It offers comprehensive features for both users and administrators.

## Technical stack

- Backend: ASP .NET Core 8.0
- Frontend: Razor Pages, HTML, CSS, JavaScript
- Database: SQL Server (ORM: EF Core)
- Testing: MSTest
- API Documentation: Swagger

## Features

### User Features

- Browse Products
- Search Products
- Add Products to Cart
- Checkout and Order Products
- View Order History

### Admin Features

- Manage Products
- Manage Categories
- Manage Brands
- Manage Orders
- Manage Users
- Pagination Support
- Docker Integration

## Project Structure

```
.
├── EcommerceMVC
│   ├── Areas
│   │   └── Admin
│   ├── Controllers
│   ├── Data
│   ├── Dtos
│   │   └── UserDtos
│   ├── Helpers
│   │   └── Extensions
│   ├── Migrations
│   ├── Models
│   │   ├── Validations
│   │   └── ViewModels
│   ├── Properties
│   ├── Services
│   │   ├── Implementations
│   │   └── Interfaces
│   ├── Views
│   │   ├── Account
│   │   ├── Brand
│   │   ├── Cart
│   │   ├── Category
│   │   ├── Home
│   │   ├── Order
│   │   ├── Product
│   │   ├── SavedAddress
│   │   ├── Shared
│   │   └── ViewComponents
│   └── wwwroot
│       ├── assets
│       ├── backend
│       ├── css
│       ├── fonts
│       ├── images
│       ├── js
│       ├── lib
│       └── media
└── EcommerceMVC.Tests
    ├── Area
    │   └── Admin
    └── Services
```

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Docker (optional, for containerization)

### Installation

1. Clone the repository:

   ```sh
   git clone https://github.com/yourusername/EcommerceMVC.git
   cd EcommerceMVC
   ```

2. Add environment configuration as `.env.sample` structure in root directory.

3. Restore the dependencies:

   ```sh
   dotnet restore
   ```

4. Run migrations:

```sh
dotnet ef database update
```

5. Build the project:

   ```sh
   dotnet build
   ```

6. Run the project:
   ```sh
   dotnet run # Run normally
   dotnet watch run # Run with hot reload
   ```

### Running with Docker

This project has been setup with the most basic Docker configuration. To run the application in a Docker container, follow these steps:

1. Build the Docker image:

   ```sh
   docker build -t ecommercemvc .
   ```

2. Run the Docker container:
   ```sh
   docker run -p 80:80 ecommercemvc
   ```

## Usage

Navigate to `http://localhost:5421` in your web browser to access the application.

## License

This project is unlicensed.
