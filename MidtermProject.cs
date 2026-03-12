using System;
using System.IO;

namespace StudentRecordSystem;
class Program
{
    static string studentFile = "students.txt";
    static string subjectFile = "subjects.txt";
    static string gradeFile = "grades.txt";

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║   ===== STUDENT RECORD SYSTEM =====   ║");   
            Console.WriteLine("║       1. Register Student             ║");
            Console.WriteLine("║       2. Enroll Student Subjects      ║");
            Console.WriteLine("║       3. Enter Grades                 ║");
            Console.WriteLine("║       4. Show Grade by Student        ║");
            Console.WriteLine("║       5. Exit                         ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");

            Console.Write("\nSelect Option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RegisterStudent();
                    break;

                case "2":
                    EnrollSubjects();
                    break;

                case "3":
                    EnterGrades();
                    break;

                case "4":
                    ShowGrades();
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    static void RegisterStudent()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTER STUDENT ===");

        string firstname = GetLettersOnly("First Name");
        string lastname = GetLettersOnly("Last Name");

        Console.Write("Middle Initial: ");
        string middleinitial = Console.ReadLine();

        Console.Write("Birthdate: ");
        string birthdate = Console.ReadLine();

        string age = GetNumbersOnly("Age");

        Console.Write("Address: ");
        string address = Console.ReadLine();

        string contact;

        while (true)
        {
            Console.Write("Contact Number: ");
            contact = Console.ReadLine();

            if (contact.Length == 11 && contact.All(char.IsDigit))
                break;

            Console.WriteLine("Invalid contact number. Must be exactly 11 digits.\n");
        }


        Console.Write("Course: ");
        string course = Console.ReadLine();

        Console.Write("Year: ");
        string year = Console.ReadLine();

        string student = firstname + "," + lastname + "," + middleinitial + "," + birthdate + "," +
                         age + "," + address + "," + contact + "," + course + "," + year;

        File.AppendAllText(studentFile, student + Environment.NewLine);

        Console.WriteLine("\nStudent Registered Successfully!");
    }

    static void EnrollSubjects()
    {
        Console.Clear();
        Console.WriteLine("=== ENROLL STUDENT SUBJECTS ===");

        string lastname = GetLettersOnly("Student Last Name");

        Console.Write("Course ID: ");
        string courseID = Console.ReadLine();

        string[,] subjects = new string[9, 2]
        {
        {"102A","Theo"},
        {"104B","IT"},
        {"101","Rizal"},
        {"106A","IT"},
        {"104A","GEC"},
        {"103A","PE"},
        {"102IT","COMP"},
        {"104A","IT"},
        {"105A","IT"}
        };

        for (int i = 0; i < 9; i++)
        {
            string subjectID = subjects[i, 0];
            string subjectName = subjects[i, 1];

            string record = lastname + "," + courseID + "," + subjectID + "," + subjectName;

            File.AppendAllText(subjectFile, record + Environment.NewLine);

            Console.WriteLine(subjectID + " - " + subjectName);
        }

        Console.WriteLine("\nAll 9 subjects automatically enrolled!");
    }

    static void EnterGrades()
    {
        Console.Clear();
        Console.WriteLine("=== ENTER GRADES ===");

        string lastname = GetLettersOnly("Student Last Name");

        Console.Write("Subject ID: ");
        string subjectID = Console.ReadLine();

        Console.Write("Grade: ");
        string grade = Console.ReadLine();

        string record = lastname + "," + subjectID + "," + grade;

        File.AppendAllText(gradeFile, record + Environment.NewLine);

        Console.WriteLine("\nGrade Saved Successfully!");
    }

    static void ShowGrades()
    {
        Console.Clear();
        Console.WriteLine("=== SHOW STUDENT GRADES ===");

        string lastname = GetLettersOnly("Enter Student Last Name");

        Console.WriteLine("\nSubjects and Grades:\n");

        if (File.Exists(gradeFile))
        {
            string[] lines = File.ReadAllLines(gradeFile);

            foreach (string line in lines)
            {
                string[] data = line.Split(',');

                if (data[0].ToLower() == lastname.ToLower())
                {
                    Console.WriteLine("Subject ID: " + data[1] + " | Grade: " + data[2]);
                }
            }
        }
        else
        {
            Console.WriteLine("No grades recorded.");
        }
    }

    static string GetLettersOnly(string fieldName)
    {
        string input;

        while (true)
        {
            Console.Write(fieldName + ": ");
            input = Console.ReadLine();

            bool valid = true;

            foreach (char c in input)
            {
                if (!char.IsLetter(c) && c != ' ')
                {
                    valid = false;
                    break;
                }
            }

            if (valid && input.Length > 0)
                return input;

            Console.WriteLine("Invalid input. Letters only.\n");
        }
    }

    static string GetNumbersOnly(string fieldName)
    {
        string input;

        while (true)
        {
            Console.Write(fieldName + ": ");
            input = Console.ReadLine();

            bool valid = true;

            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    valid = false;
                    break;
                }
            }

            if (valid && input.Length > 0)
                return input;

            Console.WriteLine("Invalid input. Numbers only.\n");
        }
    }
}