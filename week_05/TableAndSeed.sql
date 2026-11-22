CREATE DATABASE StudentManagement_SchemaTest;
GO

USE StudentManagement_SchemaTest;
GO

create table Classes(
	Id int primary key not null,
	ClassName nvarchar(50) not null
	check (datalength(trim(ClassName))>0)
);

create table Students(
	Id int primary key not null,
	StudentName nvarchar(50) not null
	check (datalength(trim(StudentName))>0),
	Age int not null
	check (Age >=16 and Age <= 100),
	ClassId int not null,
	CONSTRAINT FK_Students_Classes foreign key (ClassId) references Classes(Id)
);

create table Subjects(
	Id int primary key not null,
	SubjectName nvarchar(50) not null
	check (datalength(trim(SubjectName))>0),
	Credits int NOT NULL
	check (Credits >0)

);

SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA = 'dbo'
  AND TABLE_NAME IN ('Classes', 'Students', 'Subjects');

  SELECT name, is_disabled
FROM sys.foreign_keys
WHERE parent_object_id = OBJECT_ID(N'dbo.Students');




insert into Classes (Id, ClassName)
values (1, N'CNTT01'),(2, N'CNTT02'),(3, N'CNTT03');

insert into Subjects (Id, SubjectName, Credits)
values (1, N'C# Programming', 3), (2, N'SQL Server', 3), (3, N'ASP.NET Core', 4);

insert into Students (Id, StudentName, Age, ClassId)
values (101, N'Nguyen Van A', 20, 1),
		(102, N'Tran Thi B', 22, 1),
		(103, N'Le Van C', 19, 2),
		(104, N'Pham Thi D', 21, 2),
		(105, N'Hoang Van E', 23, 3);

select *
from Students;

select *
from Students
where Age >=21;

select *
from Students
where ClassId =1;

select * 
from Students
order by Age desc;

select *
from Students
where ClassId = 2
order by Age asc;
begin transaction;
update Students set Age=21 where Id =101;
delete from Students where Id = 105;
select * from Students;

ROLLBACK TRANSACTION;

BEGIN TRANSACTION;

UPDATE Students
SET Age = 21
WHERE Id = 101;

DELETE FROM Students
WHERE Id = 105;

SELECT Id, StudentName, Age
FROM Students
WHERE Id IN (101, 105);

ROLLBACK TRANSACTION;

SELECT Id, StudentName, Age
FROM Students
WHERE Id IN (101, 105);