using System.ComponentModel.DataAnnotations;

Istudentservice service = new StudentService();

Student s1 = new Student
{
    Id = "SV01",
    Name = "An",
    Age = 20,
    ClassName = "CNTT1",
    Score = 8.5,
};

Student s2 = new Student
{
    Id = "SV02",
    Name = "Binh",
    Age = 21,
    ClassName = "CNTT2",
    Score = 7.0,
};

Student s3 = new Student
{
    Id = "SV03",
    Name = "Cuong",
    Age = 20,
    ClassName = "CNTT1",
    Score = 9.0,
};

service.AddStudent(s1);
service.AddStudent(s2);
service.AddStudent(s3);
service.GetStudentById(s2.Id);

static void ShowMenu()
{
    Console.WriteLine("========== MENU ==========");
    Console.WriteLine("1. Them sinh vien");
    Console.WriteLine("2. Xem danh sach");
    Console.WriteLine("3. Tim sinh vien");
    Console.WriteLine("4. Cap nhat sinh vien");
    Console.WriteLine("5. Xoa sinh vien");
    Console.WriteLine("6. Thong ke so sinh vien theo lop");
    Console.WriteLine("7. Xem sinh vien co diem cao");
    Console.WriteLine("8. Thong ke diem trung binh theo lop");

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
                Console.Write("Nhap Age: ");
                string? age = Console.ReadLine();
                bool validenage = int.TryParse(age, out int agee) && agee > 0;
                Console.Write("Nhap ClassName: ");
                string? classname = Console.ReadLine();
                bool validclass = !string.IsNullOrWhiteSpace(classname);
                Console.Write("Nhap score: ");
                string? score = Console.ReadLine();
                bool validscore =
                    double.TryParse(score, out double scoree)
                    && scoree >= 0
                    && scoree <= 10
                    && double.IsFinite(scoree);

                Student std = new Student
                {
                    Id = Id,
                    Age = agee,
                    Name = name,
                    ClassName = classname,
                    Score = scoree,
                };
                if (validename && validenage && validid && validclass && validscore)
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
                    Console.WriteLine($"Class: {student.ClassName}");
                    Console.WriteLine($"Score: {student.Score}");
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
                    Console.WriteLine($"Age: {studentt.Age}");
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
                Console.Write("Nhap ClassName: ");
                classname = Console.ReadLine();
                validclass = !string.IsNullOrWhiteSpace(classname);
                Console.Write("Nhap score: ");
                score = Console.ReadLine();
                validscore =
                    double.TryParse(score, out double scoreee)
                    && scoreee >= 0
                    && scoreee <= 10
                    && double.IsFinite(scoreee);
                std = new Student
                {
                    Id = Idupdate,
                    Age = agee,
                    Name = name,
                    ClassName = classname,
                    Score = scoreee,
                };
                if (validename && validenage && validid && validclass && validscore)
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

            case "6":
                Dictionary<string, int> clasS = service.Countstudentbyclass();
                foreach (KeyValuePair<string, int> kvp in clasS)
                {
                    Console.WriteLine($"Class: {kvp.Key}, Count: {kvp.Value}");
                }
                break;

            case "7":
                Console.Write("Nhap minscore: ");
                string? minscore = Console.ReadLine();
                bool valscore =
                    double.TryParse(minscore, out double minscoree)
                    && minscoree >= 0
                    && minscoree <= 10
                    && double.IsFinite(minscoree);
                if (valscore)
                {
                    var lstudent = service.Gettopstudent(minscoree);
                    int count = 0;
                    foreach (Student student in lstudent)
                    {
                        Console.WriteLine($"Top {count + 1}: ");
                        Console.WriteLine($"Student Name: {student.Name}");
                        Console.WriteLine($"Student Class: {student.ClassName}");
                        Console.WriteLine($"Student score: {student.Score:f2}");
                        count += 1;
                    }
                }
                break;

            case "8":
                Dictionary<string, double> avgbyclass = service.Averagescorebyclass();
                foreach (KeyValuePair<string, double> kvl in avgbyclass)
                {
                    Console.WriteLine("=========================");
                    Console.WriteLine($"class: {kvl.Key} avgscore : {kvl.Value:f2}");
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
