using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class SubjectStudents
    {
        public int StudentId { get; set; }
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public DateTime ExamDate { get; set; }
        public string Mark { get; set; }
    }
}
