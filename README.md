# Employee Management System 🏢

A comprehensive web-based Employee Management System built with ASP.NET Core MVC, Entity Framework Core, and SQL Server. This application provides full CRUD operations for managing employees and departments, along with robust authentication and authorization using ASP.NET Core Identity.

![.NET](https://img.shields.io/badge/.NET-8.0-blue)


## 📋 Table of Contents

- [Features](#-features)
- [Tech Stack](#-Tech-Stack)
- [Architecture](#-architecture)
- [Getting Started](#-getting-started)
- [Project Structure](#-project-structure)
- [Database Schema](#-database-schema)
- [API Endpoints](#-api-endpoints)


## ✨ Features

### Employee Management
- ✅ Complete CRUD operations for employees
- 📸 Employee profile image upload
- 🔍 Advanced search and filtering capabilities
- 📊 Employee details with comprehensive information
- 🏢 Department assignment and management

### Department Management
- ✅ Full CRUD operations for departments
- 🔗 Employee-Department relationship tracking
- 📈 Department overview and statistics

### Authentication & Authorization
- 🔐 Secure user registration and login
- 🔑 Password reset functionality via email
- 👥 Role-based access control (RBAC)
- 🛡️ Super Admin role for system administration
- 📧 Email verification system

### User & Role Management
- 👤 User profile management
- 🎭 Dynamic role creation and assignment
- 🔧 User-role mapping interface
- 📋 Comprehensive user listing

### Technical Features
- 🎨 Responsive UI with Bootstrap 5
- ✉️ Email notifications (SMTP integration)
- 🗂️ File upload and management
- 🔄 AutoMapper for DTO mapping
- 🏗️ Repository and Unit of Work patterns
- 🎯 Lazy loading with Entity Framework proxies
- ✔️ Client and server-side validation
- 🍞 Toast notifications for user feedback

## ✔ Tech Stack

### Backend
- **Framework**: ASP.NET Core 8.0 MVC
- **ORM**: Entity Framework Core 8.0
- **Database**: SQL Server
- **Authentication**: ASP.NET Core Identity
- **Patterns**: Repository Pattern, Unit of Work, Dependency Injection

### Frontend
- **UI Framework**: Bootstrap 5
- **Icons**: Font Awesome 6.4
- **JavaScript**: jQuery
- **Validation**: jQuery Validation

### Tools & Libraries
- **Mapping**: AutoMapper 15.0
- **Email**: SMTP (System.Net.Mail)
- **File Upload**: IFormFile with validation

##  📋 Architecture

This project follows a **3-tier architecture** pattern:

```
┌─────────────────────────────────────┐
│   Presentation Layer (Demo.PL)     │
│   - Controllers                     │
│   - Views (Razor)                   │
│   - ViewModels                      │
└─────────────────────────────────────┘
              ↓
┌─────────────────────────────────────┐
│   Business Logic Layer (Demo-BLL)  │
│   - Services                        │
│   - DTOs                            │
│   - Business Rules                  │
│   - AutoMapper Profiles             │
└─────────────────────────────────────┘
              ↓
┌─────────────────────────────────────┐
│   Data Access Layer (Demo-DAL)     │
│   - DbContext                       │
│   - Entities/Models                 │
│   - Repositories                    │
│   - Migrations                      │
└─────────────────────────────────────┘
```

### Design Patterns Used
- **Repository Pattern**: Abstraction of data access logic
- **Unit of Work**: Coordinating multiple repository operations
- **Dependency Injection**: Loose coupling and testability
- **DTO Pattern**: Data transfer between layers


## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (2019 or later)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/employee-management-system.git
   cd employee-management-system
   ```


### Default Admin Account

After initial setup, create a Super Admin account through the registration page and manually assign the "Super Admin" role in the database.

## 📁 Project Structure

```
Employee-Management-System/
├── Demo.PL/                    # Presentation Layer
│   ├── Controllers/            # MVC Controllers
│   ├── Views/                  # Razor Views
│   ├── ViewModels/            # View Models
│   ├── Utilities/             # Helper Classes
│   └── wwwroot/               # Static Files
│
├── Demo-BLL/                   # Business Logic Layer
│   ├── Services/              # Service Classes
│   ├── DTOs/                  # Data Transfer Objects
│   ├── Profiles/              # AutoMapper Profiles
│   ├── Factories/             # Factory Classes
│   └── Attachment/            # File Upload Services
│
├── Demo-DAL/                   # Data Access Layer
│   ├── Data/
│   │   ├── Context/           # DbContext
│   │   ├── Configurations/    # EF Configurations
│   │   ├── Migrations/        # EF Migrations
│   │   └── Repositories/      # Repository Pattern
│   └── Models/                # Entity Models
│
└── Demo-Solution.sln          # Solution File
```

## 📁 Database Schema

### Main Tables

- **AspNetUsers**: User authentication and profile information
- **AspNetRoles**: System roles
- **AspNetUserRoles**: User-Role mapping
- **Employees**: Employee information
- **Departments**: Department information


## 🔌 API Endpoints

### Employee Controller
- `GET /Employee/Index` - List all employees
- `GET /Employee/Details/{id}` - View employee details
- `GET /Employee/Create` - Create employee form
- `POST /Employee/Create` - Create new employee
- `GET /Employee/Edit/{id}` - Edit employee form
- `POST /Employee/Edit/{id}` - Update employee
- `POST /Employee/Delete/{id}` - Delete employee

### Department Controller
- `GET /Department/Index` - List all departments
- `GET /Department/Details/{id}` - View department details
- `GET /Department/Create` - Create department form
- `POST /Department/Create` - Create new department
- `GET /Department/Edit/{id}` - Edit department form
- `POST /Department/Edit/{id}` - Update department
- `POST /Department/Delete/{id}` - Delete department

### Account Controller
- `GET /Account/Register` - Registration form
- `POST /Account/Register` - Register new user
- `GET /Account/Login` - Login form
- `POST /Account/Login` - Authenticate user
- `GET /Account/Logout` - Logout user
- `POST /Account/ForgetPassword` - Send password reset email
- `POST /Account/ResetPassword` - Reset password

### Role Controller (Super Admin Only)
- `GET /Role/Index` - List all roles
- `GET /Role/Create` - Create role form
- `POST /Role/Create` - Create new role
- `GET /Role/Edit/{id}` - Edit role form
- `POST /Role/Edit/{id}` - Update role
- `POST /Role/Delete/{id}` - Delete role

## 🔒 Security Features

- Password hashing with ASP.NET Core Identity
- Role-based authorization
- Anti-forgery token validation
- Secure password reset via email tokens
- Input validation and sanitization
- File upload validation (size, type)

## 🎯 Key Features Implementation

### File Upload
The system supports employee profile image uploads with:
- Allowed extensions: `.png`, `.jpg`, `.jpeg`
- Maximum file size: 2MB
- Unique filename generation using GUID
- Automatic directory creation

### Email Notifications
Configured SMTP for:
- Password reset emails
- Welcome emails (can be extended)
- Email templates with custom branding

### Soft Delete
Implements soft delete pattern:
- Records marked as deleted (IsDeleted flag)
- Maintains data integrity
- Allows data recovery


## 👨‍💻 Author

**Mohamed Essam**

- GitHub: [@yourusername](https://github.com/yourusername)
- Email: mido8786essam2@gmail.com



---
