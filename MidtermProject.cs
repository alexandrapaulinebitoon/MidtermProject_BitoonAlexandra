using System;
using System.IO;

namespace StudentRecordSystem;
class Program
{
    static string recordFile = "records.txt";

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

        string student = "STUDENT," + firstname + "," + lastname + "," + middleinitial + "," +
                         birthdate + "," + age + "," + address + "," + contact + "," + course + "," + year;

        File.AppendAllText(recordFile, student + Environment.NewLine);

        Console.WriteLine("\nStudent Registered Successfully!");
    }

    static void EnrollSubjects()
    {
        Console.Clear();
        Console.WriteLine("=== ENROLL 9 SUBJECTS ===");

        string lastname = GetLettersOnly("Student Last Name");

        if (!StudentExists(lastname))
        {
            Console.WriteLine("\nStudent not found. Please register the student first.");
            return;
        }

        for (int i = 1; i <= 9; i++)
        {
            Console.WriteLine("\nSubject " + i);

            Console.Write("Subject ID: ");
            string subID = Console.ReadLine();

            Console.Write("Subject Name: ");
            string subjectName = Console.ReadLine();

            string record = "SUBJECT," + lastname + "," + subID + "," + subjectName;

            File.AppendAllText(recordFile, record + Environment.NewLine);
        }

        Console.WriteLine("\nAll 9 subjects enrolled successfully!");
    }

    static void EnterGrades()
    {
        Console.Clear();
        Console.WriteLine("=== ENTER GRADES ===");

        string lname = GetLettersOnly("Student Last Name");

        if (!StudentExists(lname))
        {
            Console.WriteLine("\nStudent not found. Please register the student first.");
            return;
        }

        Console.Write("Subject ID: ");
        string subID = Console.ReadLine();

        Console.Write("Grade: ");
        string grade = Console.ReadLine();

        string record = "GRADE," + lname + "," + subID + "," + grade;

        File.AppendAllText(recordFile, record + Environment.NewLine);

        Console.WriteLine("\nGrade Saved Successfully!");
    }

    static void ShowGrades()
    {
        Console.Clear();
        Console.WriteLine("=== SHOW STUDENT GRADES ===");

        string lname = GetLettersOnly("Enter Student Last Name");

        Console.WriteLine("\nSubjects and Grades:\n");

        if (File.Exists(recordFile))
        {
            string[] lines = File.ReadAllLines(recordFile);

            foreach (string line in lines)
            {
                string[] data = line.Split(',');

                if (data[0] == "GRADE" && data[1].ToLower() == lname.ToLower())
                {
                    Console.WriteLine("Subject ID: " + data[2] + " | Grade: " + data[3]);
                }
            }
        }
        else
        {
            Console.WriteLine("No records found.");
        }
    }

    static bool StudentExists(string lastname)
    {
        if (!File.Exists(recordFile))
            return false;

        string[] lines = File.ReadAllLines(recordFile);

        foreach (string line in lines)
        {
            string[] data = line.Split(',');

            if (data[0] == "STUDENT" && data[2].ToLower() == lastname.ToLower())
            {
                return true;
            }
        }

        return false;
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
