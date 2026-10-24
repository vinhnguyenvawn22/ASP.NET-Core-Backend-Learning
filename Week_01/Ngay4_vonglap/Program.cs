// List<string> studens = new List<string>
// {
//     "An", "Binh","Chi","Dung"
// };

// Console.WriteLine("nhap ten sinh vien can tim: ");
// string? studentname = Console.ReadLine();
// bool isValidName = studentname != null && studentname.Length > 0;
// if (isValidName)
// {
//     bool existsname = studens.Contains(studentname);
//     if (existsname)
//     {
//         Console.WriteLine("Tim thay");
//     }
//     else
//     {
//         Console.WriteLine("Khong tim thay");
//     }
// }
// else
// {
//     Console.WriteLine("invalid value");
// }
// ====================================================================
// Console.WriteLine("nhap ten sinh vien muon xoa: ");
// string? name = Console.ReadLine();
// bool validnamee = name != null&& name.Length>0;
// if (validnamee)
// {
//     bool existsname = studens.Contains(name);
//     if (existsname)
//     {
//         studens.Remove(name);
//         Console.WriteLine($"da xoa {name}");
//     }
//     else
//     {
//         Console.WriteLine("Khong xoa duoc");
//     }
// }
// else
// {
//     Console.WriteLine("invalid value");
// }

// foreach (string namee in studens)
// {
//     Console.WriteLine($"student: {namee}");
// }

// ==========================================================================
// Dictionary && Hashet

// using System.Runtime.ConstrainedExecution;

// Dictionary<string,string> students = new Dictionary<string, string>();
// students.Add("SV001","An");
// students.Add("SV002","Binh");
// students.Add("SV003","Chi");

// Console.Write("Nhap ma sinh vien: ");
// string? StudentID = Console.ReadLine();
// if (StudentID != null && StudentID.Length >0)
// {
//     bool checkkey = students.ContainsKey(StudentID);
//     if (checkkey)
//     {
//         Console.WriteLine($"Name: {students[StudentID]}");
//     }
//     else
//     {
//         Console.WriteLine("Khong tim thay ID");
//     }
// }

// HashSet<string> studentidd = new HashSet<string>();
// studentidd.Add("SV001");
// studentidd.Add("SV002");
// studentidd.Add("SV001");
// Console.WriteLine($"Total unique IDs: {studentidd.Count}");

List<Student> students = new List<Student>();

Student student1 = new Student();
student1.Id = "SV001";
student1.Name = "An";
student1.Age = 20;

Student student2 = new Student();
student2.Id = "SV002";
student2.Name = "An";
student2.Age = 21;

students.Add(student1);
students.Add(student2);
foreach (Student student in students)
{
    Console.WriteLine(student.Id);
    Console.WriteLine(student.Name);
    Console.WriteLine(student.Age);
}

Console.WriteLine("nhap ma sinh vien moi: ");
string? Id = Console.ReadLine();
bool validId = Id != null && Id.Length > 0;
bool idexits = true;
if (validId)
{
    foreach (Student student in students)
    {
        if (student.Id == Id)
        {
            idexits = false;
            break;
        }
    }
    if (idexits)
    {
        Student student3 = new Student();
        student3.Name = "Minh";
        student3.Age = 22;
        student3.Id = Id;
        students.Add(student3);
    }
    foreach (Student student in students)
    {
        Console.WriteLine(student.Id);
        Console.WriteLine(student.Name);
        Console.WriteLine(student.Age);
    }
    Console.WriteLine("So sinh vien: " + students.Count);
}
else
{
    Console.WriteLine("invalid value");
}
