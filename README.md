# 🛒 E-Commerce REST API

<div align="center">

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-14.0-239120?style=for-the-badge&logo=csharp&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework%20Core-6B2C91?style=for-the-badge&logo=nuget&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-Auth-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)

A clean, modular, and production-ready **E-Commerce REST API** built with **ASP.NET Core**, following industry best practices including layered architecture, JWT authentication, the repository pattern, and Entity Framework Core.

</div>

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [Configuration](#-configuration)
- [API Endpoints](#-api-endpoints)
- [Architecture](#-architecture)
- [Contributing](#-contributing)
- [License](#-license)

---

## 🌟 Overview

This project is a fully-featured backend API for an e-commerce platform. It is designed as a **learning-focused yet production-ready template** for building scalable .NET backend services. The codebase emphasizes clean architecture, separation of concerns, and maintainability.

---

## 🚀 Features

### 🔐 Authentication & Security
- JWT-based authentication with **Access Token** and **Refresh Token** flow
- Role-based authorization with `Admin` and `User` roles
- Secure password hashing

### 🛍️ E-Commerce Modules
- **Categories** — Full CRUD operations
- **Products** — Full CRUD with image URL support
- **Cart & Cart Items** — Add, update, and remove items
- **Orders** — Create orders, view order history, and order details per user

### 🏗️ Infrastructure & Architecture
- **Layered Architecture** for clean separation of concerns
- **Generic Repository Pattern** for reusable data access logic
- **DTOs** for safe and structured input/output
- **AutoMapper** for seamless object-to-object mapping
- **Global Exception Handling Middleware** for consistent error responses
- **Custom Action Filter** for ModelState validation
- **Automatic Database Migration** on application startup

---

## 🧰 Tech Stack

| Technology | Purpose |
|---|---|
| **C# 14** | Primary language |
| **ASP.NET Core 10** | Web API framework |
| **Entity Framework Core** | ORM & database management |
| **SQL Server** | Relational database |
| **AutoMapper** | Object mapping |
| **JWT Bearer** | Authentication & authorization |

---

## 📁 Project Structure

```
E-Commerce/
├── ECommerce/                    # Single API project
│   ├── Controllers/              # API endpoints & route handlers
│   ├── Services/                 # Business logic & application services
│   ├── Repositories/             # Data access & repository implementations
│   ├── Models/                   # Domain/entity models
│   ├── DTOs/                     # Data Transfer Objects (input/output)
│   ├── Middlewares/              # Global exception handling middleware
│   ├── Filters/                  # Custom action filters (ModelState validation)
│   ├── Helpers/                  # Utility classes & AutoMapper profiles
│   ├── Data/                     # DbContext & EF Core configurations
│   ├── Migrations/               # EF Core database migrations
│   ├── appsettings.json          # App configuration
│   └── Program.cs                # Entry point, DI registration & middleware pipeline
└── ECommerce.slnx                # Solution file
```

---

## ⚡ Getting Started

### Prerequisites

Make sure you have the following installed:

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server Express)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Installation

**1. Clone the repository**
```bash
git clone https://github.com/Abd-Alrahman-Sobhy/E-Commerce.git
cd E-Commerce
```

**2. Restore dependencies**
```bash
dotnet restore
```

**3. Update the connection string** (see [Configuration](#-configuration))

**4. Run the application**
```bash
dotnet run --project ECommerce
```

The API will be available at `https://localhost:5001` (or as configured). The Swagger UI will be accessible at `/swagger`.

> **Note:** The database is created and migrated automatically on first startup.

---

## ⚙️ Configuration

Update `appsettings.json` with your environment-specific values:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JWT": {
    "Key": "your-super-secret-key-here",
    "Issuer": "ECommerceAPI",
    "Audience": "ECommerceClient",
    "DurationInDays": 7
  }
}
```

| Key | Description |
|---|---|
| `ConnectionStrings:DefaultConnection` | SQL Server connection string |
| `JWT:Key` | Secret key for signing JWT tokens (use a strong, random value) |
| `JWT:Issuer` | Token issuer identifier |
| `JWT:Audience` | Token audience identifier |
| `JWT:DurationInDays` | Token validity period in days |

---

## 📡 API Endpoints

### 🔐 Auth
| Method | Endpoint | Description | Access |
|---|---|---|---|
| `POST` | `/api/auth/register` | Register a new user | Public |
| `POST` | `/api/auth/login` | Login and receive tokens | Public |
| `POST` | `/api/auth/refresh-token` | Refresh access token | Public |

### 📦 Categories
| Method | Endpoint | Description | Access |
|---|---|---|---|
| `GET` | `/api/categories` | Get all categories | Public |
| `GET` | `/api/categories/{id}` | Get category by ID | Public |
| `POST` | `/api/categories` | Create a new category | Admin |
| `PUT` | `/api/categories/{id}` | Update a category | Admin |
| `DELETE` | `/api/categories/{id}` | Delete a category | Admin |

### 🛍️ Products
| Method | Endpoint | Description | Access |
|---|---|---|---|
| `GET` | `/api/products` | Get all products | Public |
| `GET` | `/api/products/{id}` | Get product by ID | Public |
| `POST` | `/api/products` | Create a new product | Admin |
| `PUT` | `/api/products/{id}` | Update a product | Admin |
| `DELETE` | `/api/products/{id}` | Delete a product | Admin |

### 🛒 Cart
| Method | Endpoint | Description | Access |
|---|---|---|---|
| `GET` | `/api/cart` | Get current user's cart | User |
| `POST` | `/api/cart/items` | Add item to cart | User |
| `PUT` | `/api/cart/items/{id}` | Update cart item quantity | User |
| `DELETE` | `/api/cart/items/{id}` | Remove item from cart | User |

### 📋 Orders
| Method | Endpoint | Description | Access |
|---|---|---|---|
| `GET` | `/api/orders` | Get current user's orders | User |
| `GET` | `/api/orders/{id}` | Get order details | User |
| `POST` | `/api/orders` | Place a new order | User |

> **Note:** Full Swagger documentation is available at `/swagger` when running the application.

---

## 🏛️ Architecture

This project is a **single ASP.NET Core API** organized into feature-focused folders within one project. Each folder has a clear, dedicated responsibility:

| Folder | Responsibility |
|---|---|
| `Controllers/` | Handle HTTP requests, delegate to services, return responses |
| `Services/` | Contain business logic and orchestrate data operations |
| `Repositories/` | Abstract database access via the Repository Pattern |
| `Models/` | Define domain entities mapped to database tables |
| `DTOs/` | Shape data going in and out of the API |
| `Middlewares/` | Handle cross-cutting concerns like global exception handling |
| `Filters/` | Intercept requests for validation (e.g. ModelState checks) |
| `Helpers/` | Hold AutoMapper profiles and shared utility classes |
| `Data/` | DbContext and EF Core entity configurations |

**Key design decisions:**
- The **Repository Pattern** abstracts data access, keeping controllers and services free from raw EF Core queries.
- **DTOs** prevent over-posting and decouple the API contract from internal domain models.
- **Global Exception Middleware** ensures all errors return consistent, structured JSON responses.
- **AutoMapper** profiles keep mapping logic centralized and out of controllers.
- **Custom Action Filters** handle ModelState validation uniformly across all endpoints.

---

## 📄 License

This project is open-source and available under the [MIT License](LICENSE).

---

<div align="center">

Made with ❤️ by [Abd-Alrahman Sobhy](https://github.com/Abd-Alrahman-Sobhy)

⭐ If you find this project helpful, please consider giving it a star!

</div>
