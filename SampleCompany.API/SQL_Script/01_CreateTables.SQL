
IF DB_ID('CompanyDB') IS NOT NULL
BEGIN
  DROP DATABASE CompanyDB;
END;
GO

CREATE DATABASE CompanyDB;
GO

USE [CompanylDB]
GO
--Create Department Table
CREATE TABLE Department
(
  DepartmentId INT PRIMARY KEY IDENTITY(1,1),
  DepartmentName NVARCHAR(100) NOT NULL
);
GO
--Create Employee Table
CREATE TABLE Employee(
  EmployeeID INT PRIMARY KEY IDENTITY(1,1),
  Name NVARCHAR(100) NOT NULL,
  Email NVARCHAR(100),
  DepartmentID INT FOREIGN KEY REFERENCES Department(DepartmentId)
);
GO
--Create Project Table
CREATE TABLE Project(
ProjectID INT PRIMARY KEY IDENTITY(1,1),
ProjectName NVARCHAR(100) NOT NULL
);
GO
--Create Employee&Project Table
CREATE TABLE EmployeeProject(
EmployeeID INT,
ProjectID INT,
PRIMARY KEY (EmployeeID , ProjectID),
FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID) ,
FOREIGN KEY (ProjectID) REFERENCES Project(ProjectID) ,
);