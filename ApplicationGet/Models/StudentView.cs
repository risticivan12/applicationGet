using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationGet.Models
{
    [System.Runtime.InteropServices.Guid("61116727-6263-4CF3-86ED-1724DAA5056F")]
    public class StudentView
    {
        public int StudentId { get; set; }
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }

        public static implicit operator StudentView(Student student)
        {
            if (student == null)
                return null;
            else
            {
                return new StudentView()
                {
                    StudentId = student.StudentId,
                    IndexNumber = student.IndexNumber,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    City = student.City
                };
            }
        }

        public override string ToString()
        {
            return StudentId + "," + IndexNumber + "," + FirstName + "," + LastName + "," + City;

        }
    }
}
