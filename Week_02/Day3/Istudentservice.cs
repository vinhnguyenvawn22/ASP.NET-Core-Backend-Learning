interface Istudentservice
{
    bool AddStudent(Student student);

    List<Student> GetStudents();

    bool Updatestudent(Student student);

    bool Deletestudent(string Id);

    Student? GetStudentById(string id);

    Dictionary<string, int> Countstudentbyclass();

    List<Student> Gettopstudent(double minscore);

    Dictionary<string, double> Averagescorebyclass();

}