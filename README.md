# 🎓 Student Management API (.NET 8)

A production-ready REST API built using ASP.NET Core with Clean Architecture principles.

---

## 🚀 Features

- CRUD Operations (Students)
- Database First (EF Core)
- JWT Authentication (Login/Register)
- Refresh Tokens
- Role & Claims based Authorization
- AutoMapper
- FluentValidation
- Pagination & Filtering
- Serilog Logging
- Exception Handling Middleware

---

## 🏗️ Architecture

Controller → Service → Repository → Database

- Separation of Concerns
- Dependency Injection
- DTO-based design

---

## 🛠️ Tech Stack

- ASP.NET Core (.NET 8)
- Entity Framework Core
- SQL Server / LocalDB
- AutoMapper
- FluentValidation
- Serilog
- JWT Authentication

---

## 📦 Setup Instructions

### 1️⃣ Clone Repo
git clone <your-repo-url>
cd StudentManagement

### 2️⃣ Configure DB
Update appsettings.json:

"ConnectionStrings": {
  "Default": "Server=(localdb)\\MSSQLLocalDB;Database=StudentDB;Trusted_Connection=True;"
}

### 3️⃣ Run Project
dotnet run

---

## 🔐 Authentication APIs

POST /auth/register  
POST /auth/login  

---

## 📚 Student APIs

GET /students  
GET /students/{id}  
POST /students  
PUT /students/{id}  
DELETE /students/{id}  

---

## 📊 Pagination Example

GET /students?page=1&pageSize=10&city=Hyd

---

## ⚡ EF Core Concepts Demonstrated

- DbContext & DbSet
- Database First
- Lazy Loading vs Eager Loading
- Include()
- Tracking vs NoTracking
- N+1 Problem

---

## 🧠 Best Practices

- Use DTOs instead of Entities
- Use AsNoTracking for read-only queries
- Avoid N+1 problem using Include
- Use structured logging

---

## 📁 Logs

/Logs/log-*.txt

---

## 👨‍💻 Author

Harshith

---

## ⭐ Status

✔ Production Ready Backend API
