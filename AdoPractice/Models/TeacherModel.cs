using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdoPractice.Models
{
    public class TeacherModel
    {
        public TeacherModel(int id, string firstName, string lastName, int salary, DateTime birthDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Salary = salary;
            BirthDate = birthDate;
        }

        public TeacherModel()
        {

        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
        public DateTime BirthDate { get; set; }
    }
}