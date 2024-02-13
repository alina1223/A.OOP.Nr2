using System;
using System.Collections.Generic;
using System.Linq;

public enum StudyField
{
    MECHANICAL_ENGINEERING = 1,
    SOFTWARE_ENGINEERING = 2,
    FOOD_TECHNOLOGY = 3,
    URBANISM_ARCHITECTURE = 4,
    VETERINARY_MEDICINE = 5
}

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public DateTime DateOfBirth { get; set; }

    public Student(string firstName, string lastName, string email, DateTime enrollmentDate, DateTime dateOfBirth)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        EnrollmentDate = enrollmentDate;
        DateOfBirth = dateOfBirth;
    }
}

public class Faculty
{
    public string Name { get; set; }
    public string Abbreviation { get; set; }
    public StudyField StudyField { get; set; }
    public List<Student> Students { get; set; }

    public Faculty(string name, string abbreviation, StudyField studyField)
    {
        Name = name;
        Abbreviation = abbreviation;
        StudyField = studyField;
        Students = new List<Student>();
    }

    public void AddStudent(Student student)
    {
        Students.Add(student);
    }

    public void GraduateStudent(Student student)
    {
        Students.Remove(student);
    }

    public void DisplayStudents(IEnumerable<Student> students)
    {
        foreach (var student in students)
        {
            Console.WriteLine($"{student.FirstName} {student.LastName} ({student.Email})");
        }
    }
}

public class University
{
    public List<Faculty> Faculties { get; set; }

    public University()
    {
        Faculties = new List<Faculty>();
    }

    public Faculty CreateFaculty(string name, string abbreviation, StudyField studyField)
    {
        var newFaculty = new Faculty(name, abbreviation, studyField);
        Faculties.Add(newFaculty);
        return newFaculty;
    }

    public Faculty SearchFacultyByStudentIdentifier(string identifier)
    {
        return Faculties.Find(faculty => faculty.Students.Any(student => student.Email == identifier));
    }

    public void DisplayAllFaculties()
    {
        foreach (var faculty in Faculties)
        {
            Console.WriteLine($"{faculty.Name} ({faculty.Abbreviation}) - {faculty.StudyField}");
        }
    }

    public void DisplayFacultiesByField(StudyField studyField)
    {
        var matchingFaculties = Faculties.FindAll(faculty => faculty.StudyField == studyField);
        DisplayFaculties(matchingFaculties);
    }

    public void DisplayCurrentEnrolledStudents()
    {
        Console.WriteLine("\nCurrent Enrolled Students:");
        foreach (var faculty in Faculties)
        {
            Console.WriteLine($"\n{faculty.Name} ({faculty.Abbreviation}) - {faculty.StudyField}");
            faculty.DisplayStudents(faculty.Students);
        }
    }

    public void DisplayGraduates()
    {
        Console.WriteLine("\nGraduates:");
        foreach (var faculty in Faculties)
        {
            Console.WriteLine($"\n{faculty.Name} ({faculty.Abbreviation}) - {faculty.StudyField}");
            faculty.DisplayStudents(faculty.Students);
        }
    }

    private void DisplayFaculties(IEnumerable<Faculty> faculties)
    {
        foreach (var faculty in faculties)
        {
            Console.WriteLine($"{faculty.Name} ({faculty.Abbreviation}) - {faculty.StudyField}");
        }
    }
}

class Program
{
    static void Main()
    {
        University university = new University();

        while (true)
        {
            Console.WriteLine("\n===== TUM Student Management System =====");
            Console.WriteLine("Faculty Operations:");
            Console.WriteLine("1. Create and assign a student to a faculty.");
            Console.WriteLine("2. Graduate a student from a faculty.");
            Console.WriteLine("3. Display current enrolled students.");
            Console.WriteLine("4. Display graduates.");
            Console.WriteLine("5. Check if a student belongs to a faculty.");
            Console.WriteLine("\nGeneral Operations:");
            Console.WriteLine("6. Create a new faculty.");
            Console.WriteLine("7. Search what faculty a student belongs to by email.");
            Console.WriteLine("8. Display University faculties.");
            Console.WriteLine("9. Display faculties belonging to a field.");
            Console.WriteLine("0. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddStudentToFaculty(university);
                    break;

                case "2":
                    GraduateStudent(university);
                    break;

                case "3":
                    university.DisplayCurrentEnrolledStudents();
                    break;

                case "4":
                    university.DisplayGraduates();
                    break;

                case "5":
                    CheckStudentBelongsToFaculty(university);
                    break;

                case "6":
                    CreateNewFaculty(university);
                    break;

                case "7":
                    SearchFacultyByStudentEmail(university);
                    break;

                case "8":
                    university.DisplayAllFaculties();
                    break;

                case "9":
                    DisplayFacultiesByField(university);
                    break;

                case "0":
                    Console.WriteLine("Exiting program. Goodbye!");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }

    private static void AddStudentToFaculty(University university)
    {
        Console.Write("Enter student's first name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter student's last name: ");
        string lastName = Console.ReadLine();
        Console.Write("Enter student's email: ");
        string email = Console.ReadLine();
        DateTime enrollmentDate = DateTime.Now;
        Console.Write("Enter student's date of birth (YYYY-MM-DD): ");
        DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());
        Student student = new Student(firstName, lastName, email, enrollmentDate, dateOfBirth);
        Console.WriteLine("\nAvailable Faculties:");
        university.DisplayAllFaculties();
        Console.Write("Enter the name of the faculty to assign the student: ");
        string facultyName = Console.ReadLine();
        var matchingFaculties = university.Faculties.FindAll(faculty => faculty.Name == facultyName);

        if (matchingFaculties.Count > 0)
        {
            matchingFaculties[0].AddStudent(student);
            Console.WriteLine($"\n{student.FirstName} {student.LastName} ({student.Email}) assigned to {facultyName}.");
        }
        else
        {
            Console.WriteLine($"\nFaculty '{facultyName}' not found.");
        }
    }

    private static void GraduateStudent(University university)
    {
        Console.Write("Enter student's email to graduate: ");
        string graduateEmail = Console.ReadLine();
        Faculty graduateFaculty = university.SearchFacultyByStudentIdentifier(graduateEmail);

        if (graduateFaculty != null)
        {
            var matchingStudents = graduateFaculty.Students.FindAll(student => student.Email == graduateEmail);
            if (matchingStudents.Count > 0)
            {
                graduateFaculty.GraduateStudent(matchingStudents[0]);
                Console.WriteLine($"\n{matchingStudents[0].FirstName} {matchingStudents[0].LastName} ({graduateEmail}) graduated from {graduateFaculty.Name}.");
            }
            else
            {
                Console.WriteLine($"\nStudent with email '{graduateEmail}' not found in {graduateFaculty.Name}.");
            }
        }
        else
        {
            Console.WriteLine($"\nStudent with email '{graduateEmail}' not found in any faculty.");
        }
    }

    private static void CheckStudentBelongsToFaculty(University university)
    {
        Console.Write("Enter student's email to check if they belong to a faculty: ");
        string checkEmail = Console.ReadLine();
        Faculty checkFaculty = university.SearchFacultyByStudentIdentifier(checkEmail);
        if (checkFaculty != null)
        {
            Console.WriteLine($"\n{checkEmail} belongs to {checkFaculty.Name} ({checkFaculty.Abbreviation}).");
        }
        else
        {
            Console.WriteLine($"\n{checkEmail} does not belong to any faculty.");
        }
    }

    private static void CreateNewFaculty(University university)
    {
        Console.Write("Enter new faculty name: ");
        string newFacultyName = Console.ReadLine();
        Console.Write("Enter new faculty abbreviation: ");
        string newFacultyAbbreviation = Console.ReadLine();
        Console.WriteLine("\nAvailable Study Fields:");
        foreach (var field in Enum.GetValues(typeof(StudyField)))
        {
            Console.WriteLine(field);
        }
        Console.Write("Enter the study field for the new faculty: ");
        StudyField newStudyField = (StudyField)Enum.Parse(typeof(StudyField), Console.ReadLine());
        Faculty newFaculty = university.CreateFaculty(newFacultyName, newFacultyAbbreviation, newStudyField);
        Console.WriteLine($"\nNew faculty created: {newFaculty.Name} ({newFaculty.Abbreviation}) - {newFaculty.StudyField}.");
    }

    private static void SearchFacultyByStudentEmail(University university)
    {
        Console.Write("Enter student's email to search for the faculty: ");
        string searchEmail = Console.ReadLine();
        Faculty searchFaculty = university.SearchFacultyByStudentIdentifier(searchEmail);
        if (searchFaculty != null)
        {
            Console.WriteLine($"\n{searchEmail} belongs to {searchFaculty.Name} ({searchFaculty.Abbreviation}).");
        }
        else
        {
            Console.WriteLine($"\nStudent with email '{searchEmail}' not found in any faculty.");
        }
    }

    private static void DisplayFacultiesByField(University university)
    {
        Console.WriteLine("\nAvailable Study Fields:");
        foreach (var field in Enum.GetValues(typeof(StudyField)))
        {
            Console.WriteLine(field);
        }
        Console.Write("Enter the study field to display faculties: ");
        StudyField displayStudyField = (StudyField)Enum.Parse(typeof(StudyField), Console.ReadLine());
        Console.WriteLine($"\nFaculties belonging to {displayStudyField}:");
        university.DisplayFacultiesByField(displayStudyField);
    }
}
