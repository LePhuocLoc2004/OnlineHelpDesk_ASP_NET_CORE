# OnlineHelpDesk_ASP_NET_CORE

🏥 Online Help Desk System
ASP.NET Core MVC + Entity Framework Core
📌 Project Overview

This project is a Role-Based Online Help Desk Management System developed using ASP.NET Core MVC and Entity Framework Core.

The system allows employees to submit support requests, administrators to manage users, and support staff to handle assigned requests.

The application implements 3 user roles:

👑 Admin

🛠 Support Staff

👤 Employee

🎯 Main Features
👑 Admin

🔐 Login with default admin account

👥 View employee list

👁 View requests of selected employee

➕ Create employee accounts (Employee / Support Staff)

📌 Assign requests to support staff

🔎 Search requests by:

Time range

Priority level

🛠 Support Staff

🔐 Login with assigned account

📋 View requests assigned to them

🔎 Search assigned requests by:

Time range

Priority

✏ Update personal account information

👤 Employee

🔐 Login with assigned account

📝 Create support requests

Select priority from DoUuTien table

Assigned support staff = NULL when created

📂 View personal requests

🔎 Search requests by:

Time range

Priority

✏ Update personal account information

🗄 Database Design

Main tables include:

NhanVien (with role column: quyen)

YeuCau

DoUuTien

Related entities for request management

Role values:

0 → Employee

1 → Support Staff

2 → Admin

🛠 Tech Stack
🔥 Backend

C#

ASP.NET Core MVC

Entity Framework Core

LINQ

SQL Server

🎨 Frontend

Razor View Engine

HTML5

CSS3

Bootstrap

JavaScript

🗄 Database

Microsoft SQL Server

🧰 Tools

Visual Studio

SQL Server Management Studio (SSMS)

NuGet Package Manager

🏗 Architecture

MVC Pattern

Role-Based Authorization

Entity Framework Core (Code First / Database First)

Layout-based UI structure

🚀 Project Purpose

This system simulates a real-world enterprise support management workflow where:

Employees create support requests

Admin assigns requests

Support staff processes and resolves them

It demonstrates:

Authentication & Authorization

Role management

CRUD operations

Filtering & Searching

Database relationship handling
