# ADNManagerment_SE1855

## Bloodline DNA Testing Service Management System

### 📋 Project Overview
A comprehensive .NET WPF application for managing DNA testing services, supporting multiple user roles and complete CRUD operations.

### 🏗️ Architecture
- **Multi-layered Architecture**: BusinessObjects, DataAccessObjects, Repositories, Services, WPF UI
- **Database**: SQL Server with Entity Framework Core
- **Framework**: .NET 8.0 WPF

### 👥 User Roles
1. **Guest**: View home page and services
2. **Customer**: Book services, view results, send feedback
3. **Staff**: Manage bookings, add results, view customer data
4. **Manager**: All staff permissions + user management, reports
5. **Admin**: Full system access and management

### 🚀 Features

#### Core Functionality
- ✅ **User Authentication & Authorization**
- ✅ **Service Booking Management**
- ✅ **Test Process Tracking**
- ✅ **Result Management**
- ✅ **Feedback System**
- ✅ **User Profile Management**
- ✅ **Dashboard & Reports**

#### Technical Features
- ✅ **Role-based Access Control (RBAC)**
- ✅ **Multi-layered Architecture**
- ✅ **Entity Framework Core Integration**
- ✅ **Modern WPF UI/UX**
- ✅ **Error Handling & Validation**
- ✅ **Database Relationships**

### 📁 Project Structure
```
ADNManagerment_SE1855/
├── BusinessObjects/          # Entity models
├── DataAccessObjects/        # Database operations
├── Repositories/            # Data abstraction layer
├── Services/               # Business logic layer
├── ADNManagermentWPF/      # WPF UI application
├── ADNManagerment.sql      # Database schema
└── README.md              # This file
```

### 🛠️ Technologies Used
- **.NET 8.0**
- **WPF (Windows Presentation Foundation)**
- **Entity Framework Core**
- **SQL Server**
- **C#**
- **XAML**

### 🚀 Getting Started

#### Prerequisites
- Visual Studio 2022 or .NET 8.0 SDK
- SQL Server (LocalDB or Express)
- Windows OS

#### Installation
1. **Clone the repository**
2. **Run database script**: Execute `ADNManagerment.sql` in SQL Server
3. **Build the solution**: `dotnet build ADNManagerment_SE1855.sln`
4. **Run the application**: `dotnet run --project ADNManagermentWPF`

#### Default Login Credentials
- **Admin**: admin@adntest.com / admin123
- **Manager**: manager@adntest.com / manager123
- **Staff**: staff@adntest.com / staff123
- **Customer**: customer@adntest.com / customer123

### 📊 Database Schema
- **Users**: User management and authentication
- **Services**: Available DNA testing services
- **Bookings**: Service bookings and appointments
- **Results**: Test results and outcomes
- **Feedbacks**: Customer feedback and ratings

### 🎯 Key Features by Role

#### Customer Features
- Book DNA testing services
- View booking status and history
- Access test results
- Send feedback and ratings
- Manage profile information

#### Staff Features
- View and manage bookings
- Add and update test results
- View customer information
- Track test progress

#### Manager Features
- All staff permissions
- User management (except admin)
- View reports and statistics
- Service management

#### Admin Features
- Full system access
- User management (all roles)
- System configuration
- Complete CRUD operations

### 🔧 Development

#### Building the Project
```bash
dotnet build ADNManagerment_SE1855.sln
```

#### Running the Application
```bash
dotnet run --project ADNManagermentWPF
```

#### Database Setup
1. Open SQL Server Management Studio
2. Execute `ADNManagerment.sql` script
3. Update connection string if needed

### 📝 Notes
- Built with clean architecture principles
- Comprehensive error handling
- Modern UI/UX design
- Production-ready codebase
- Scalable and maintainable

### 👨‍💻 Developer
**Nguyen Tang Minh Thong - SE1855**

### 📅 Version
**1.0.0** - Production Ready

---
*Bloodline DNA Testing Service Management System - Professional Grade Application* 