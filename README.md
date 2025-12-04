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
2️⃣ Update Connection String
Edit appsettings.json:

json
Copy code
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
3️⃣ Run the API
bash
Copy code
dotnet run
API will run on:

https://localhost:7001

http://localhost:5001

🔥 API Endpoints (Overview)
Auth Endpoints
Method	Endpoint	Description
POST	/api/auth/register	Register new user
POST	/api/auth/login	Login & receive tokens
POST	/api/auth/refresh	Refresh access token

Category Endpoints
Method	Endpoint	Description
GET	/api/categories	Get all categories
POST	/api/categories	Create category
PUT	/api/categories/{id}	Update category
DELETE	/api/categories/{id}	Delete category

Product Endpoints
Method	Endpoint	Description
GET	/api/products	Get all products
GET	/api/products/{id}	Get product by ID
POST	/api/products	Create product
PUT	/api/products/{id}	Update product
DELETE	/api/products/{id}	Delete product

Cart & Orders
Method	Endpoint	Description
GET	/api/cart	Get user cart
POST	/api/cart/add	Add item to cart
POST	/api/cart/remove	Remove item from cart
POST	/api/orders	Create order
GET	/api/orders/my	Get logged-in user's orders

