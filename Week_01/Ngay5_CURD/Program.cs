List<Student> students = new List<Student> { };

Student student1 = new Student();
student1.Name = "An";
student1.Age = 20;
student1.Id = "SV001";

Student student2 = new Student();
student2.Name = "Binh";
student2.Age = 21;
student2.Id = "SV002";

Student student3 = new Student();
student3.Name = "Minh";
student3.Age = 22;
student3.Id = "SV003";

students.Add(student1);
students.Add(student2);
students.Add(student3);

static void ShowMenu()
{
    Console.WriteLine("========== MENU ==========");
    Console.WriteLine("1. Them sinh vien");
    Console.WriteLine("2. Xem danh sach");
    Console.WriteLine("3. Tim sinh vien");
    Console.WriteLine("4. Cap nhat sinh vien");
    Console.WriteLine("5. Xoa sinh vien");
    Console.WriteLine("0. Thoat");
    Console.Write("Nhap lua chon: ");
}


static void showstudens(List<Student> students)
{
    foreach (Student student in students)
    {
        Console.WriteLine("ID: " + student.Id);
        Console.WriteLine("Name: " + student.Name);
        Console.WriteLine("Age: " + student.Age);
        Console.WriteLine("=================");
    }
    Console.WriteLine($"so luong sinh vien: {students.Count}");
}

static void AddStudent(List<Student> students)
{
    Console.Write("Nhap ID new: ");
    string? Id = Console.ReadLine();
    bool valueId = !string.IsNullOrWhiteSpace(Id);
    bool havekey = true;
    if (valueId)
    {
        foreach (Student student in students)
        {
            if (student.Id == Id)
            {
                havekey = false;
            }
        }
        if (havekey)
        {
            Console.Write("Nhap Name: : ");
            string? name = Console.ReadLine();
            bool validename = !string.IsNullOrWhiteSpace(name);
            Console.Write("Nhap Age: : ");
            string? age = Console.ReadLine();
            bool validenage = int.TryParse(age, out int agee) && agee > 0;
            if (validename && validenage)
            {
                Student student = new Student();
                student.Age = agee;
                student.Id = Id;
                student.Name = name;
                students.Add(student);
            }
            else
            {
                Console.WriteLine("invalid value");
            }
        }
        else
        {
            Console.WriteLine("ID đã tồn tại, không thể thêm sinh viên.");
        }
    }
    else
    {
        Console.WriteLine("ID khong hop le");
    }
}

static void UpdateStudent(List<Student> students)
{
    Console.Write("Nhap Id can sua: ");
    string? id = Console.ReadLine();
    bool valididd = !string.IsNullOrWhiteSpace(id);
    bool flag = false;
    int count = 0;
    if (valididd)
    {
        foreach (Student student in students)
        {
            if (student.Id == id)
            {
                flag = true;
                break;
            }
            count += 1;
        }
        if (flag)
        {
            Console.WriteLine($"student ID: {id}");
            Console.WriteLine($"Nhap new name:");
            string? name = Console.ReadLine();
            bool validname = !string.IsNullOrWhiteSpace(name);
            Console.WriteLine($"Nhap new Age: {id}");
            string? age = Console.ReadLine();
            bool validage = int.TryParse(age, out int agee) && agee > 0;
            if (validage && validname)
            {
                students[count].Name = name;
                students[count].Age = agee;
                Console.WriteLine("Da sua thong tin thanh cong");
            }
            else
            {
                Console.WriteLine("Invalid age or name");
            }
        }
        else
        {
            Console.WriteLine("Id khong ton tai");
        }
    }
    else
    {
        Console.WriteLine("invalid ID");
    }
}

static void DeleteStudent(List<Student> students)
{
    Console.Write("nhap ID can xoa: ");
    string? iddelete = Console.ReadLine();
    bool checkid = !string.IsNullOrWhiteSpace(iddelete);
    int count = 0;
    bool flag = false;
    if (checkid)
    {
        foreach (Student student in students)
        {
            if (student.Id == iddelete)
            {
                flag = true;
                break;
            }
            count += 1;
        }
        if (flag)
        {
            students.Remove(students[count]);
            Console.WriteLine("da xoa thanh cong");
        }
        else
        {
            Console.WriteLine("khong tim thay sinh vien");
        }
    }
    else
    {
        Console.WriteLine("Invalid id");
    }
}

static void SearchStudent(List<Student> students)
{
    Console.Write("Nhap ID can tim: ");
    string? Idd = Console.ReadLine();
    bool valueIdd = !string.IsNullOrWhiteSpace(Idd);
    bool havekeyy = false;
    int count = 0;
    if (valueIdd)
    {
        foreach (Student student in students)
        {
            if (student.Id == Idd)
            {
                havekeyy = true;
                break;
            }
            count += 1;
        }
        if (havekeyy)
        {
            Console.WriteLine($"Student Id: {students[count].Id}");
            Console.WriteLine($"Student age: {students[count].Age}");
            Console.WriteLine($"Student name: {students[count].Name}");
        }
        else
        {
            Console.WriteLine("khong tim thay sinh vien");
        }
    }
    else
    {
        Console.WriteLine("ID không hợp lệ.");
    }
}

bool isrunning = true;
while (isrunning)
{
    ShowMenu();
    Console.WriteLine();
    string? choice = Console.ReadLine();
    bool validChoice = !string.IsNullOrWhiteSpace(choice);
    if (validChoice)
    {
        switch (choice)
        {
            case "1":
                AddStudent(students);
                break;
            case "2":
                showstudens(students);
                break;
            case "3":
                SearchStudent(students);
                break;
            case "4":
                UpdateStudent(students);
                break;

            case "5":
                DeleteStudent(students);

                break;
            case "0":
                isrunning = false;
                Console.WriteLine("chuong trinh ket thuc");
                break;
            default:
                Console.WriteLine("lua chon khong hop le");
                break;
        }
    }
    else
    {
        Console.WriteLine("invalid choice");
    }
}
