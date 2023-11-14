using JsonSerializeTask.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JsonSerializeTask.Services
{
    internal static class StudentServices
    {
        static ICollection<Student> Students = new List<Student>();
        //i was curious how to get path of Program.cs https://stackoverflow.com/a/70724207
        static string programClassPath = Path.Combine(new ArraySegment<string>(System.AppContext.BaseDirectory.Split(Path.DirectorySeparatorChar), 0, System.AppContext.BaseDirectory.Split(Path.DirectorySeparatorChar).Length - 4).ToArray());
        static string studentJson = Path.Combine(Path.GetFullPath(programClassPath) + @"\Data\studentJson.json");

        public static void InitializeStudents()
        {
            //if file does not exist create it + write there an empty json list
            if (!File.Exists(studentJson))
            {
                File.Create(studentJson).Close();
                using (var sw = new StreamWriter(studentJson))
                {
                    sw.Write(JsonConvert.SerializeObject(Students));
                }
            }
        }

        public static void AddStudent(Student student)
        {
            using (var sr  = new StreamReader(studentJson)) 
            {
                string jsonContent = sr.ReadToEnd();
                Students = JsonConvert.DeserializeObject<ICollection<Student>>(jsonContent);
            }

            Students.Add(student);
            using (var sw = new StreamWriter(studentJson)) 
            {
                sw.Write(JsonConvert.SerializeObject(Students));
            }
        }
        public static List<Student> GetStudents() 
        {
            using (var sr = new StreamReader(studentJson))
            {
                string jsonContent = sr.ReadToEnd();
                Students = JsonConvert.DeserializeObject<ICollection<Student>>(jsonContent);
            }
            return Students.ToList();
        }
        public static void RemoveStudent(string code)
        {
            using (var sr = new StreamReader(studentJson))
            {
                string jsonContent = sr.ReadToEnd();
                Students = JsonConvert.DeserializeObject<ICollection<Student>>(jsonContent);
            }
            Students.Remove(Students.FirstOrDefault(student => student.Code == code));
            using (var sw = new StreamWriter(studentJson))
            {
                sw.Write(JsonConvert.SerializeObject(Students));
            }
        }
        public static void EditStudent(string code, Student student)
        {
            using (var sr = new StreamReader(studentJson))
            {
                string jsonContent = sr.ReadToEnd();
                Students = JsonConvert.DeserializeObject<ICollection<Student>>(jsonContent);
            }
            var userToEdit = Students.FirstOrDefault(student => student.Code == code);
            if (!String.IsNullOrEmpty(student.Name))
            {
                userToEdit.Name = student.Name;
            }
            if (!String.IsNullOrEmpty(student.Surname))
            {
                userToEdit.Surname = student.Surname;
            }
            if (!String.IsNullOrEmpty(student.Code))
            {
                userToEdit.Code = student.Code;
            }
            using (var sw = new StreamWriter(studentJson))
            {
                sw.Write(JsonConvert.SerializeObject(Students));
            }
        }
    }
}
