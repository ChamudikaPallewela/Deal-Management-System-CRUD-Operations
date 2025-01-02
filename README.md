# Deal Management System

## Overview
This project is a Deal Management System built using **ASP.NET Core** and **Entity Framework Core** with **SQL Server** as the database. It supports the management of deals, associated hotels, and multimedia content, with full CRUD (Create, Read, Update, Delete) functionality.

## Features
- **CRUD Operations**:
  - Manage deals (Create, Read, Update, Delete).
  - Manage hotels linked to deals.
- **Role-Based Data Relationships**:
  - Deals can include one or more hotels.
- **Video Management**:
  - Deals can store a video URL.
- **API Documentation**:
  - Swagger UI enabled for API testing.

## Technologies Used
- **Backend**: ASP.NET Core 6.0
- **Database**: SQL Server (via SQL Server Management Studio)
- **ORM**: Entity Framework Core
- **Validation**: Fluent Validation
- **API Documentation**: Swagger/OpenAPI

---

## Requirements
1. **Development Environment**:
   - Visual Studio 2022 or higher
   - .NET SDK 6.0 or later
   - SQL Server and SQL Server Management Studio (SSMS)

2. **NuGet Packages**:
   - `Microsoft.EntityFrameworkCore.SqlServer`
   - `Microsoft.EntityFrameworkCore.Tools`
   - `Microsoft.EntityFrameworkCore.Design`
   - `Swashbuckle.AspNetCore`
   - `FluentValidation.AspNetCore`

---

## Database Schema
### **Deals Table**
| Column Name    | Data Type        | Constraints                     |
|----------------|------------------|---------------------------------|
| Id             | int              | Primary Key, Identity (Auto-Increment) |
| Name           | nvarchar(100)    | NOT NULL                       |
| Slug           | nvarchar(100)    | NOT NULL, UNIQUE               |
| Video          | nvarchar(255)    | NULL                           |

### **Hotels Table**
| Column Name    | Data Type        | Constraints                     |
|----------------|------------------|---------------------------------|
| Id             | int              | Primary Key, Identity (Auto-Increment) |
| Name           | nvarchar(100)    | NOT NULL                       |
| Rate           | decimal(3,1)     | NOT NULL, Range: 1.0 - 5.0     |
| Amenities      | nvarchar(MAX)    | NULL                           |
| DealId         | int              | Foreign Key References `Deals(Id)` |

---

## Project Setup
### **1. Clone the Repository**
```bash
git clone https://github.com/ChamudikaPallewela/Deal-Management-System-CRUD-Operations
cd Deal-Management-System-CRUD-Operations
cd ASE
```

### **2. Set Up the Database**
1. Open **SQL Server Management Studio (SSMS)**.
2. Create a new database:
   ```sql
   CREATE DATABASE DealManagementDB;
   ```
3. Use the provided SQL script to create the tables:
   ```sql
   USE DealManagementDB;
   
   CREATE TABLE Deals (
       Id INT IDENTITY(1,1) PRIMARY KEY,
       Name NVARCHAR(100) NOT NULL,
       Slug NVARCHAR(100) NOT NULL UNIQUE,
       Video NVARCHAR(255) NULL
   );

   CREATE TABLE Hotels (
       Id INT IDENTITY(1,1) PRIMARY KEY,
       Name NVARCHAR(100) NOT NULL,
       Rate DECIMAL(3,1) NOT NULL CHECK (Rate >= 1.0 AND Rate <= 5.0),
       Amenities NVARCHAR(MAX) NULL,
       DealId INT NULL,
       FOREIGN KEY (DealId) REFERENCES Deals(Id) ON DELETE CASCADE
   );
   ```

### **3. Configure the Connection String**
1. Open `appsettings.json`.
2. Update the `DefaultConnection` value with your SQL Server details:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=DealManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

### **4. Install Dependencies**
1. Open the terminal in Visual Studio.
2. Run:
   ```bash
   dotnet restore
   ```

### **5. Run the Application**
1. Build and run the project in Visual Studio.
2. Navigate to `https://localhost:<port>/swagger` to view and test the API.

---

## API Endpoints
### **Deals API**
| HTTP Method | Endpoint           | Description                     |
|-------------|--------------------|---------------------------------|
| GET         | /api/deals         | Retrieve all deals              |
| GET         | /api/deals/{id}    | Retrieve a specific deal by ID  |
| POST        | /api/deals         | Create a new deal               |
| PUT         | /api/deals/{id}    | Update an existing deal         |
| DELETE      | /api/deals/{id}    | Delete a deal by ID             |

### **Hotels API**
| HTTP Method | Endpoint           | Description                     |
|-------------|--------------------|---------------------------------|
| GET         | /api/hotels        | Retrieve all hotels             |
| GET         | /api/hotels/{id}   | Retrieve a specific hotel by ID |
| POST        | /api/hotels        | Create a new hotel              |
| PUT         | /api/hotels/{id}   | Update an existing hotel        |
| DELETE      | /api/hotels/{id}   | Delete a hotel by ID            |

---

## Testing the API
### **Using Postman**
1. **Install Postman**: [Download here](https://www.postman.com/downloads/).
2. Use the following example requests:

#### **1. GET /api/deals**
- Request Type: `GET`
- URL: `https://localhost:<port>/api/deals`

#### **2. POST /api/deals**
- Request Type: `POST`
- URL: `https://localhost:<port>/api/deals`
- Headers:
  - `Content-Type: application/json`
- Body (JSON):
   ```json
   {
       "name": "Holiday Special",
       "slug": "holiday-special",
       "video": "https://example.com/video",
       "hotels": [
           {
               "name": "Luxury Inn",
               "rate": 4.5,
               "amenities": "WiFi,Pool,Gym"
           }
       ]
   }
   ```

---

## Troubleshooting
### **Common Issues**
1. **Cyclical Reference Error**:
   - Ensure the following is set in `Program.cs`:
     ```csharp
     builder.Services.AddControllers().AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
     });
     ```

2. **Database Connection Issues**:
   - Verify the connection string in `appsettings.json`.
   - Confirm SQL Server is running.


