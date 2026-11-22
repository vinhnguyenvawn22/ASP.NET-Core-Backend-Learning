USE StudentManagement_SchemaTest;
GO

CREATE OR ALTER PROCEDURE usp_GetAllStudents
AS
BEGIN
    SELECT *
    FROM Students
    ORDER BY Id ASC;
END;
GO

exec usp_GetAllStudents;

go

create or alter procedure usp_GetStudentById
@StudentId int
as
begin
select Id, StudentName, Age, ClassId
from Students
where Id = @StudentId
end;

go
exec usp_GetStudentById @StudentId = 101;
exec usp_GetStudentById @StudentId = 102;
exec usp_GetStudentById @StudentId = 999;

go

create or alter procedure usp_CountStudents
    @TotalStudents int output
as
begin
select @TotalStudents = count(*)
from Students
end;

go

declare @result int;
exec usp_CountStudents @TotalStudents = @result output;

select @result as TotalStudents

SELECT COUNT(*) AS ActualCount
FROM Students;

go

create or alter procedure usp_AddStudent
@Id int,
@StudentName nvarchar(50),
@Age int,
@ClassId int
as 
begin
    insert into Students (Id, StudentName, Age, ClassId)
    values (@Id, @StudentName, @Age, @ClassId)
end;

go

begin transaction;

exec usp_AddStudent 
@Id = 301,
@StudentName = N'Nguyen Van Test',
@Age = 20,
@ClassId = 1;

select Id, StudentName, Age, ClassId
from Students
where Id = 301;

rollback transaction

select *
from Students
where Id = 301;

go 

create or alter procedure usp_UpdateStudentAge
@StudentId INT, @NewAge INT
as
begin
    update Students 
    set Age = @NewAge 
    where Id = @StudentId
end;

go


BEGIN TRANSACTION;

EXEC usp_UpdateStudentAge
    @StudentId = 101,
    @NewAge = 21;

SELECT Id, StudentName, Age
FROM Students
WHERE Id = 101;

ROLLBACK TRANSACTION;

SELECT Id, StudentName, Age
FROM Students
WHERE Id = 101;

go

create or alter procedure usp_DeleteStudent
@StudentId INT
as 
begin
    delete from Students
    where Id = @StudentId
end;

go

begin transaction;

exec usp_DeleteStudent @StudentId = 105

select *
from Students
where Id = 105
rollback transaction
select *
from Students
where Id = 105;

go

create or alter procedure usp_GetStudentsPaged
@PageNumber INT, @PageSize INT
as begin
select Id, StudentName, Age, ClassId
from Students
order by Id asc
offset (@PageNumber -1)*@PageSize rows
fetch next @PageSize rows only
end;

go

exec usp_GetStudentsPaged
@PageNumber =1, @PageSize = 2;

exec usp_GetStudentsPaged
@PageNumber =3, @PageSize = 2;