# AgadeApp 3

App allows users to input file entries and upload files to them. Different things can be achieved based on authorization level

## App Functions

- Adding file entries
- Uploading .pdf files
- Downloading .pdf files
- Registration and Login features
- Admin control over file entries

# Installation and required packages

All packages were installed with the use of **NuGet**.
App uses an external database, in this case **Microsoft SQL Server**

## Packages

- Swashbuckle.AspNetCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools

## Installation

- In the file **appsettings.json** there are three connection strings with database address set to (localdb) - internal DB for Visual Studio, you are free to change them to suit Your needs
- Using the package manager console (NuGet)
- Initialize the database as You see fit or follow the next 2 steps:
- Create 2 empty databases on Your server named: **FileDatabase** and **UserDatabase**
- Run these commands in order: 
**add-migration InitialUser -context UserDbContext**
**add-migration InitialFile -context FileDbContext**
**update-database -context UserDbContext**
**update-database -context FileDbContext**

# Aviailable Users

## Admins

- **Login:** admin1@fake.fake **Password:** AdminPassword1

## Users

- **Login:** test1@fake.fake **Password:** Password1
- **Login:** test2@fake.fake **Password:** Password2
