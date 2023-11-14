using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerializeTask.Models
{
    internal class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Code { get; set; }
        public override string ToString()
        {
            return $"\n{Name}, {Surname}, {Code}";
        }
    }
}
