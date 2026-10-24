using System.Data.SqlTypes;

static double CalculateAverage(double Math, double literature, double english)
{
    double ava = (Math + literature + english) / 3;
    return ava;
}

static void showStudentResult(double Math, double literature, double english)
{
    double ava = CalculateAverage(Math, literature, english);
    bool passed = ava >= 5 && Math >= 4 && literature >= 4 && english >= 4;
    string result = "";
    if (passed)
    {
        result = "Passed";
    }
    else
    {
        result = "Failed";
    }
    Console.WriteLine($"Averate: {ava}");
    Console.WriteLine($"passed: {result}");
}

Console.WriteLine("===== STUDENT MANAGEMENT =====");
static void Showmenu()
{
    Console.WriteLine("1. Check Student Result");
    Console.WriteLine("2. Classify Student Grade");
    Console.WriteLine("0. Exit");
    Console.WriteLine("Enter your choice:");
}

while (true)
{
    Showmenu();
    string? choice = Console.ReadLine();
    bool validchoice = int.TryParse(choice, out int choicee);
    if (!validchoice)
    {
        Console.WriteLine("Invalid value");
        continue;
    }
    else if (choicee == 0)
    {
        Console.WriteLine("Good bye");
        break;
    }
    if (validchoice)
    {
        switch (choicee)
        {
            case 1:
                Console.Write("Nhap Math: ");
                string? Math = Console.ReadLine();

                bool validMath =
                    double.TryParse(Math, out double Mathh)
                    && double.IsFinite(Mathh)
                    && Mathh >= 0
                    && Mathh <= 10;
                Console.Write("Nhap Literature: ");
                string? Literature = Console.ReadLine();

                bool validaLiterature =
                    double.TryParse(Literature, out double Literaturee)
                    && double.IsFinite(Literaturee)
                    && Literaturee >= 0
                    && Literaturee <= 10;
                Console.Write("Nhap English: ");
                string? English = Console.ReadLine();
                bool validEnglish =
                    double.TryParse(English, out double Englishh)
                    && double.IsFinite(Englishh)
                    && Englishh >= 0
                    && Englishh <= 10;
                if (validMath && validEnglish && validaLiterature)
                {
                    showStudentResult(Mathh, Literaturee, Englishh);
                }
                else
                {
                    Console.WriteLine("Invalid score.");
                }

                break;
            case 2:
                Console.Write("Nhap diem trung binh: ");
                string? average = Console.ReadLine();

                bool validavarage =
                    double.TryParse(average, out double avrage)
                    && double.IsFinite(avrage)
                    && avrage >= 0
                    && avrage <= 10;
                string grade = "";
                if (validavarage)
                {
                    if (avrage >= 8)
                    {
                        grade = "Excellent";
                    }
                    else if (avrage >= 6.5 && avrage < 8)
                    {
                        grade = "Good";
                    }
                    else if (avrage >= 5 && avrage < 6.5)
                    {
                        grade = "Average";
                    }
                    else
                    {
                        grade = "Poor";
                    }
                }
                if (grade != null && grade.Length == 0)
                {
                    Console.WriteLine($"Invalid score.");
                }
                else
                {
                    Console.WriteLine($"Grade: {grade}");
                }

                break;
            case 3:
                Console.WriteLine("Search Student selected");
                break;
            case 0:
                Console.WriteLine("Exit selected");
                break;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }
    else
    {
        Console.WriteLine("Invalid value");
    }
}
