using JsonSerializeTask.Models;
using JsonSerializeTask.Services;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace JsonSerializeTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StudentServices.InitializeStudents();
            
            while (true)
            {
                Console.WriteLine("Select from options: \n1. Add new student\n2. See all students\n3. Remove student by code\n4. Edit student by code\n0. Exit from program");
                char option = Console.ReadKey(intercept: true).KeyChar;
                switch (option)
                {
                    case '1':
                        Console.Write("\nEnter name of student: ");
                        string nameToAdd = Console.ReadLine();
                        Console.Write("\nEnter surname of student: ");
                        string surnameToAdd = Console.ReadLine();
                        Console.Write("\nEnter unique code of student(any characters): ");
                        string codeToAdd = Console.ReadLine();
                        if (StudentServices.GetStudents().FirstOrDefault(student => student.Code.Equals(codeToAdd))  != null)
                        {
                            Console.WriteLine("\nCode must be unique! Try again");
                            Console.WriteLine();
                            break;
                        }
                        StudentServices.AddStudent(new Student { Name = nameToAdd, Surname = surnameToAdd, Code = codeToAdd });
                        Console.WriteLine();
                        break;
                    case '2':
                        StudentServices.GetStudents().ForEach(student => { Console.WriteLine(student); });
                        Console.WriteLine();
                        break;
                    case '3':
                        Console.Write("\nEnter code of student to remove: ");
                        string codeToRemove = Console.ReadLine();
                        if (StudentServices.GetStudents().FirstOrDefault(student => student.Code.Equals(codeToRemove)) == null)
                        {
                            Console.WriteLine("\nUser with this code does not exist! Try again");
                            Console.WriteLine();
                            break;
                        }
                        StudentServices.RemoveStudent(codeToRemove);
                        Console.WriteLine();
                        break;
                    case '4':
                        Console.Write("\nEnter code of student to edit: ");
                        string codeToEdit = Console.ReadLine();
                        if (StudentServices.GetStudents().FirstOrDefault(student => student.Code.Equals(codeToEdit)) == null)
                        {
                            Console.WriteLine("\nUser with this code does not exist! Try again");
                            Console.WriteLine();
                            break;
                        }
                        Console.Write("\nEnter new name(keep empty if you dont want to change it): ");
                        string newName = Console.ReadLine();
                        Console.Write("\nEnter new surname(keep empty if you dont want to change it): ");
                        string newSurname = Console.ReadLine();
                        Console.Write("\nEnter new name(keep empty if you dont want to change it): ");
                        string newCode = Console.ReadLine();
                        if (StudentServices.GetStudents().FirstOrDefault(student => student.Code.Equals(newCode)) != null)
                        {
                            Console.WriteLine("\nCode must be unique! Try again");
                            Console.WriteLine();
                            break;
                        }
                        StudentServices.EditStudent(codeToEdit, new Student
                        {
                            Name = newName, Surname = newSurname, Code = newCode 
                        });
                        Console.WriteLine();
                        break;
                    case '0':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nNo such option!");
                        Console.WriteLine();
                        break;

                }
            }
        }
    }
}