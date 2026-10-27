interface Istudentservice
{
    bool AddStudent(Student student);

    List<Student> GetStudents();

    bool Updatestudent(Student student);

    bool Deletestudent(string Id);

    Student? GetStudentById(string id);

}