using System.Diagnostics;

class StudentService : Istudentservice
{
    private List<Student> students = new List<Student>();

    public bool AddStudent(Student exitstudent)
    {
        bool valueId = !string.IsNullOrWhiteSpace(exitstudent.Id);
        if (valueId)
        {
            foreach (Student studentt in students)
            {
                if (studentt.Id == exitstudent.Id)
                {
                    return false;
                }
            }
            students.Add(exitstudent);
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<Student> GetStudents()
    {
        return students;
    }

    public bool Updatestudent(Student student)
    {
        bool valueId = !string.IsNullOrWhiteSpace(student.Id);
        if (valueId)
        {
            foreach (Student existingStudent in students)
            {
                if (existingStudent.Id == student.Id)
                {
                    existingStudent.Age = student.Age;
                    existingStudent.Name = student.Name;
                    existingStudent.ClassName = student.ClassName;
                    existingStudent.Score = student.Score;
                    return true;
                }
            }
            return false;
        }

        return false;
    }

    public bool Deletestudent(string Id)
    {
        bool validid = !string.IsNullOrWhiteSpace(Id);
        if (validid)
        {
            bool flag = false;
            int count = 0;
            foreach (Student exitst in students)
            {
                if (exitst.Id == Id)
                {
                    flag = true;
                    break;
                }
                count += 1;
            }
            if (flag)
            {
                students.Remove(students[count]);
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public Student? GetStudentById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return null;

        foreach (Student student in students)
        {
            if (student.Id == id)
            {
                return student;
            }
        }

        return null;
    }

    public Dictionary<string, int> Countstudentbyclass()
    {
        Dictionary<string, int> result = new();

        var groups = students.GroupBy(s => s.ClassName);

        foreach (var group in groups)
        {
            result[group.Key] = group.Count();
        }

        return result;
    }

    public List<Student> Gettopstudent(double minscore)
    {   
        var result = students.Where(x => x.Score>=minscore)
        .OrderByDescending(x=> x.Score).ToList();


        return result;
    }

    public Dictionary<string, double> Averagescorebyclass()
    {
        var groupclass = students.GroupBy(x => x.ClassName).ToList();
        Dictionary<string, double> result = new Dictionary<string, double>();

        foreach (var group in groupclass)
        {
            double avg = group.Average(x => x.Score);
            result[group.Key] = avg;
        }

        return result;
    }
}
