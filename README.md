🏥 Online Help Desk System
📌 Overview

Online Help Desk Management System developed using ASP.NET Core MVC and Entity Framework Core. The system supports 3 roles: Admin, Support Staff, and Employee. It allows employees to submit support requests, admins to manage accounts and assign requests, and support staff to process assigned tickets.

👑 Admin

Login with default admin account, view employee list and their requests, create employee or support accounts, assign requests to support staff, and search requests by time range or priority.

🛠 Support Staff

Login with admin-created account, view assigned requests, search by time range or priority, and update personal account information.

👤 Employee

Login with admin-created account, create support requests with selected priority (assigned support staff = NULL initially), view personal requests, search by time range or priority, and update account information.

🗄 Database

Main tables include NhanVien (with role column: quyen), YeuCau, DoUuTien. Role values: 0 = Employee, 1 = Support Staff, 2 = Admin.

🛠 Tech Stack

Backend: C#, ASP.NET Core MVC, Entity Framework Core, SQL Server
Frontend: Razor View Engine, HTML5, CSS3, Bootstrap, JavaScript
Tools: Visual Studio, SQL Server Management Studio (SSMS), NuGet

🎯 Project Purpose

Simulates a real-world enterprise support management workflow with role-based authorization, CRUD operations, filtering/searching features, and MVC architecture implementation.
