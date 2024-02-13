

using System;

public interface ILogger
{
    void LogOperation(string operation, string details);
    void LogError(string errorMessage);
}

public class FileLogger : ILogger
{
    private const string LogFilePath = "log.txt";

    public void LogOperation(string operation, string details)
    {
        string logEntry = $"{DateTime.Now}: [{operation}] - {details}";
        WriteToFile(logEntry);
        Console.WriteLine(logEntry);
    }

    public void LogError(string errorMessage)
    {
        string errorEntry = $"{DateTime.Now}: [ERROR] - {errorMessage}";
        WriteToFile(errorEntry);
        Console.WriteLine(errorEntry);
    }

    private void WriteToFile(string entry)
    {
        File.AppendAllText(LogFilePath, entry + Environment.NewLine);
    }
}

public class Faculty
{
    public string Name { get; set; }
    public string Abbreviation { get; set; }
    public string Field { get; set; }
}

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
}

public class StudentManager
{
    private readonly ILogger logger;
    private readonly Faculty faculty;

    public StudentManager(ILogger logger, Faculty faculty)
    {
        this.logger = logger;
        this.faculty = faculty;
    }

    public void CreateStudent(string firstName, string lastName, string email, int day, int month, int year)
    {
       
        var student = new Student
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            BirthDate = new DateTime(year, month, day)
        };

        
        logger.LogOperation("CreateStudent", $"{student.FirstName} {student.LastName} ({student.Email})");
    }

   

    public void DisplayEnrolledStudents()
    {
        
        logger.LogOperation("DisplayEnrolledStudents", $"Displaying enrolled students for {faculty.Abbreviation}");
    }
}

public class Program
{
    static void Main()
    {
      
        ILogger logger = new FileLogger();
        Faculty faculty = new Faculty { Name = "Media si Telecomunicatii", Abbreviation = "MTC", Field = "SOFTWARE_ENGINEERING" };
        StudentManager studentManager = new StudentManager(logger, faculty);

        
        while (true)
        {
            Console.WriteLine("Welcome to TUM's student management system!");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("g - General operations");
            Console.WriteLine("f - Faculty operations");
            Console.WriteLine("s - Student operations");
            Console.Write("your input> ");

            string input = Console.ReadLine();

            switch (input.ToLower())
            {
                case "g":
                    GeneralOperationsMenu(studentManager, logger);
                    break;
                case "f":
                    FacultyOperationsMenu(studentManager);
                    break;
                case "s":
                    StudentOperationsMenu(studentManager);
                    break;
                case "q":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }

    static void GeneralOperationsMenu(StudentManager studentManager, ILogger logger)
    {
        while (true)
        {
            Console.WriteLine("General operations");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("nf/<faculty name>/<faculty abbreviation>/<field> - create faculty");
            Console.WriteLine("ss/<student email> - search student and show faculty");
            Console.WriteLine("df - display faculties");
            Console.WriteLine("df/<field> - display all faculties of a field");
            Console.WriteLine("b - Back");
            Console.WriteLine("q - Quit Program");
            Console.Write("your input> ");

            string input = Console.ReadLine();

            switch (input.ToLower())
            {
                case "nf":
                   ъ
                    break;
                case "ss":
                    
                    break;
                case "df":
                   
                    break;
                case "b":
                    return;
                case "q":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }

    static void FacultyOperationsMenu(StudentManager studentManager)
    {
        while (true)
        {
            Console.WriteLine("Faculty operations");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("ns/<faculty abbreviation>/<first name>/<last name>/<email>/<day>/<month>/<year> - create student");
            Console.WriteLine("gs/<email> - graduate student");
            Console.WriteLine("ds - display enrolled students");
            Console.WriteLine("dg - display graduated students");
            Console.WriteLine("bf/<faculty abbreviation>/<email> - check if student belongs to faculty");
            Console.WriteLine("b - Back");
            Console.WriteLine("q - Quit Program");
            Console.Write("your input> ");

            string input = Console.ReadLine();

            switch (input.ToLower())
            {
                case "ns":
                   
                    break;
                case "gs":
                    
                    break;
                case "ds":
                    studentManager.DisplayEnrolledStudents();
                    break;
                case "dg":
                    
                    break;
                case "bf":
                  
                    break;
                case "b":
                    return;
                case "q":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }

    static void StudentOperationsMenu(StudentManager studentManager)
    {
    }
}
