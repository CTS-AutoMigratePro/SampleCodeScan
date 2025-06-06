
IF DB_ID('CompanyDB') IS NOT NULL
BEGIN
  DROP DATABASE CompanyDB;
END;
GO

CREATE DATABASE CompanyDB;
GO


USE [CompanyDB]
GO
--INSERT Department
CREATE PROCEDURE spInsertDepartment
  @DepartmentName NVARCHAR(100)
AS
BEGIN
   INSERT INTO Department (DepartmentName) VALUES (@DepartmentName);
END
GO

--INSERT Employee
CREATE PROCEDURE spInsertEmployee
  @Name NVARCHAR(100),
  @Email NVARCHAR(100),
  @DepId INT
AS
BEGIN
   INSERT INTO Employee (Name,Email,DepartmentID) VALUES (@Name,@Email,@DepId);
END
GO

--GET ALL Employee
CREATE PROCEDURE spGetAllEmployee
AS
BEGIN
  SELECT e.EmployeeID,e.Name,e.Email , d.DepartmentName
  FROM Employee e
  JOIN Department d ON e.DepartmentId = d.DepartmentID;
END
GO

--Update Employee
CREATE PROCEDURE spUpdateEmployee
   @EmployeeID INT,
   @Name NVARCHAR(100),
   @Email NVARCHAR(100),
   @DepId INT
AS
BEGIN
  UPDATE Employee
  SET Name = @Name,
      Email = @Email,
	  DepartmentID = @DepId
  WHERE EmployeeID = @EmployeeID;
END
GO

--DELETE Employee
CREATE PROCEDURE spDeleteEmployee
  @EmployeeID INT
AS
BEGIN
  DELETE FROM Employee 
  WHERE EmployeeID = @EmployeeID;
END
GO

--INSERT Project
CREATE PROCEDURE spInsertProject
   @ProjectName NVARCHAR(100)
AS
BEGIN
   INSERT INTO Project (ProjectName) VALUES (@ProjectName);
END
GO

--Assign a project to employee
CREATE PROCEDURE spAssignProject
  @ProjectId INT,
  @EmployeeID INT
AS
BEGIN
   INSERT INTO EmployeeProject (ProjectID,EmployeeID) VALUES (@ProjectId,@EmployeeID);
END
GO

-- Get Employee with project
CREATE PROCEDURE spGetEmployeeWithProject
 AS
 BEGIN
    SELECT e.Name , e.Email , ep.ProjectID , P.ProjectName
	FROM Employee e
	JOIN EmployeeProject ep ON e.EmployeeID = ep.EmployeeID
	JOIN Project P ON ep.ProjectID = P.ProjectID;
END
GO

--Nested SP : Create Employee and AssignProject
CREATE PROCEDURE spCreateEmpAndAssignProject
  @Name NVARCHAR(100),
  @Email NVARCHAR(100),
  @DepId INT,
  @ProjectID INT
AS
BEGIN
   EXEC spInsertEmployee @Name ,@Email , @DepId;
   DECLARE @NewEmployeeID INT;
   SELECT TOP 1 @NewEmployeeID = EmployeeID FROM Employee ORDER BY EmployeeID DESC;
   EXEC spAssignProject @ProjectID , @NewEmployeeID ;
END
GO