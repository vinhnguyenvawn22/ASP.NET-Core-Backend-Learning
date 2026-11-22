USE StudentManagement_SchemaTest;
GO

select  cl.ClassName,
		st.StudentName,
		st.Age
from Students st
inner join Classes cl
on st.ClassId = cl.Id
order by st.StudentName asc;


select cl.ClassName,
      count(st.Id) TotalStudents
from Classes cl
left join Students st
on st.ClassId = cl.Id
group by cl.ClassName, cl.Id

select cl.ClassName,
		avg(cast(st.Age as decimal(10,2))) as AverageAge,
		MIN(st.Age) as MinAge,
		MAX(st.Age) as MaxAge
from Classes cl
left join Students st
on st.ClassId = cl.Id
group by cl.ClassName, cl.Id;

GO

create or alter view vw_ClassStudentCount
as 
select cl.ClassName,
		COUNT(st.Id) as TotalStudents
from Classes cl
left join Students st
on st.ClassId = cl.Id
group by cl.ClassName, cl.Id;

GO

SELECT *
FROM vw_ClassStudentCount;

