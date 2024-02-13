using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public DateTime DateOfBirth { get; set; }
}

enum StudyField
{
    MECHANICAL_ENGINEERING,
    SOFTWARE_ENGINEERING,
    FOOD_TECHNOLOGY,
    URBANISM_ARCHITECTURE,
    VETERINARY_MEDICINE
}

class Faculty
{
    public string Name { get; set; }
    public string Abbreviation { get; set; }
    public List<Student> Students { get; set; }
    public StudyField StudyField { get; set; }
}

class FileManager
{
    public static void SaveState(List<Faculty> faculties)
    {
        var data = new
        {
            faculties = faculties.Select(f => new
            {
                name = f.Name,
                abbreviation = f.Abbreviation,
                students = f.Students.Select(s => new
                {
                    first_name = s.FirstName,
                    last_name = s.LastName,
                    email = s.Email,
                    enrollment_date = s.EnrollmentDate.ToString("yyyy-MM-dd"),
                    date_of_birth = s.DateOfBirth.ToString("yyyy-MM-dd")
                })
            })
        };

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText("state.json", json);
    }

    public static List<Faculty> LoadState()
    {
        try
        {
            if (File.Exists("state.json"))
            {
                string json = File.ReadAllText("state.json");
                var data = JsonConvert.DeserializeAnonymousType(json, new { faculties = new List<object>() });

                var faculties = new List<Faculty>();
                foreach (var facultyData in data.faculties)
                {
                    var faculty = new Faculty
                    {
                        Name = facultyData.name,
                        Abbreviation = facultyData.abbreviation,
                        Students = new List<Student>(),
                        StudyField = StudyField.MECHANICAL_ENGINEERING 
                    };

                    foreach (var studentData in facultyData.students)
                    {
                        var student = new Student
                        {
                            FirstName = studentData.first_name,
                            LastName = studentData.last_name,
                            Email = studentData.email,
                            EnrollmentDate = DateTime.ParseExact(studentData.enrollment_date, "yyyy-MM-dd", null),
                            DateOfBirth = DateTime.ParseExact(studentData.date_of_birth, "yyyy-MM-dd", null)
                        };
                        faculty.Students.Add(student);
                    }

                    faculties.Add(faculty);
                }

                return faculties;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading state: {ex.Message}");
        }

        return new List<Faculty>();
    }
}

class Program
{
    static void Main()
    {
        List<Faculty> faculties = FileManager.LoadState();

        while (true)
        {
            Console.WriteLine("Welcome to TUM's student management system!");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("g - General operations");
            Console.WriteLine("f - Faculty operations");
            Console.WriteLine("s - Student operations");
            Console.Write("your input> ");
            string userInput = Console.ReadLine()?.ToLower();

            if (userInput == "g")
            {
                
                Console.WriteLine("General operations");
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("nf/<faculty name>/<faculty abbreviation>/<field> - create faculty");
                Console.WriteLine("ss/<student email> - search student and show faculty");
                Console.WriteLine("df - display faculties");
                Console.WriteLine("df/<field> - display all faculties of a field");
                Console.WriteLine("q - Quit Program");
                Console.Write("your input> ");
                string generalInput = Console.ReadLine()?.ToLower();

                
            }
            else if (userInput == "f")
            {
                
                Console.WriteLine("Faculty operations");
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("ns/<faculty abbreviation>/<first name>/<last name>/<email>/<day>/<month>/<year> - create student");
                Console.WriteLine("gs/<email> - graduate student");
                Console.WriteLine("ds/<faculty abbreviation> - display enrolled students");
                Console.WriteLine("dg/<faculty abbreviation> - display graduated students");
                Console.WriteLine("bf/<faculty abbreviation>/<email> - check if student belongs to faculty");
                Console.WriteLine("b - Back");
                Console.WriteLine("q - Quit Program");
                Console.Write("your input> ");
                string facultyInput = Console.ReadLine()?.ToLower();

            }
            else if (userInput == "s")
            {
                
                Console.WriteLine("Student operations");
                Console.Write("Your input> s");
               
            }
            else if (userInput == "q")
            {
                FileManager.SaveState(faculties);
                Console.WriteLine("Exiting the program. State saved.");
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'g', 'f', 's', or 'q'.");
            }
        }
    }
}

