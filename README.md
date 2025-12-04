# 🛒 E-Commerce API (ASP.NET Core)

A clean and modular **E-Commerce REST API** built with **ASP.NET Core**, following best practices such as layered architecture, DTOs, validation, AutoMapper, JWT authentication, repository pattern, and Entity Framework Core.

This project is designed as a learning-focused yet production-ready template for building scalable .NET backend APIs.

---

## 🚀 Features

### 🔐 Authentication & Authorization
- JWT Authentication (Access Token + Refresh Token)
- Role-based authorization (`Admin`, `User`)
- Secure password hashing

### 🛍️ E-Commerce Core
- Categories CRUD
- Products CRUD (with Image URL)
- Cart + CartItems
- Orders (Create, List, Details)
- Order history per user

### 🗄️ Infrastructure
- Entity Framework Core with SQL Server  
- Generic Repository Pattern  
- DTOs for input/output  
- AutoMapper integration  
- Global exception handling middleware  
- Custom ModelState validation using Action Filter  
- Automatic database migration on startup  

---

## 🧰 Tech Stack

- **C# 14**
- **ASP.NET Core 10**
- **Entity Framework Core**
- **SQL Server**
- **AutoMapper**
- **JWT Authentication**

---

## ⚙️ How to Run

### 1️⃣ Clone the Project
```bash
git clone https://github.com/Abd-Alrahman-Sobhy/E-Commerce.git
cd E-Commerce
```

### 2️⃣ Update Connection String
```bash
Edit appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3️⃣ Run the API
```bash
dotnet run
```

🔥 API Endpoints (soon)
