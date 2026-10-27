Istudentservice service = new StudentService();

Student s1 = new Student
{
    Id = "SV01",
    Name = "An",
    Age = 29,
};
Student s2 = new Student
{
    Id = "SV02",
    Name = "vinh",
    Age = 21,
};

service.AddStudent(s1);
service.AddStudent(s2);
service.AddStudent(s1);
service.GetStudentById(s2.Id);

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
                Console.Write("Nhap ID new: ");
                string? Id = Console.ReadLine();
                bool validid = !string.IsNullOrWhiteSpace(Id);
                Console.Write("Nhap Name: : ");
                string? name = Console.ReadLine();
                bool validename = !string.IsNullOrWhiteSpace(name);
                Console.Write("Nhap Age: : ");
                string? age = Console.ReadLine();
                bool validenage = int.TryParse(age, out int agee) && agee > 0;
                Student std = new Student
                {
                    Id = Id,
                    Age = agee,
                    Name = name,
                };
                if (validename && validenage && validid)
                {
                    bool result = service.AddStudent(std);

                    if (result)
                    {
                        Console.WriteLine("Them sinh vien thanh cong");
                    }
                    else
                    {
                        Console.WriteLine("Them sinh vien that bai");
                    }
                }
                else
                {
                    Console.WriteLine("Du lieu khong hop le");
                }

                break;
            case "2":
                List<Student> students = service.GetStudents();
                foreach (Student student in students)
                {
                    Console.WriteLine($"ID: {student.Id}");
                    Console.WriteLine($"Age: {student.Age}");
                    Console.WriteLine($"Name: {student.Name}");
                    Console.WriteLine("==========================");
                }
                Console.WriteLine($"so luong sinh vien: {students.Count}");
                break;
            case "3":
                Console.Write("Nhap ID can tim: ");
                string? FindId = Console.ReadLine();
                validid = !string.IsNullOrWhiteSpace(FindId);
                Student? studentt = service.GetStudentById(FindId);
                if (studentt != null)
                {
                    Console.WriteLine($"ID: {studentt.Id}");
                    Console.WriteLine($"Name: {studentt.Name}");
                    Console.WriteLine($"ID: {studentt.Age}");
                }
                break;
            case "4":
                Console.Write("Nhap ID update: ");
                string? Idupdate = Console.ReadLine();
                validid = !string.IsNullOrWhiteSpace(Idupdate);
                Console.Write("Nhap Name: : ");
                name = Console.ReadLine();
                validename = !string.IsNullOrWhiteSpace(name);
                Console.Write("Nhap Age: : ");
                age = Console.ReadLine();
                validenage = int.TryParse(age, out agee) && agee > 0;
                std = new Student
                {
                    Id = Idupdate,
                    Age = agee,
                    Name = name,
                };
                if (validename && validenage && validid)
                {
                    bool result = service.Updatestudent(std);

                    if (result)
                    {
                        Console.WriteLine("cap nhat sinh vien thanh cong");
                    }
                    else
                    {
                        Console.WriteLine("cap nhat sinh vien that bai");
                    }
                }
                else
                {
                    Console.WriteLine("Du lieu khong hop le");
                }

                break;

            case "5":
                Console.Write("Nhap ID can xoa: ");
                string? Idremove = Console.ReadLine();
                validid = !string.IsNullOrWhiteSpace(Idremove);
                if (validid)
                {
                    bool result = service.Deletestudent(Idremove);
                    if (result)
                    {
                        Console.WriteLine("xoa sinh vien thanh cong");
                    }
                    else
                    {
                        Console.WriteLine("xoa sinh vien that bai");
                    }
                }
                else
                {
                    Console.WriteLine("Du lieu khong hop le");
                }

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
