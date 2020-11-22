using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Data.Entities
{
    public class Exam
    {
        [Key]
        public int ExamId { get; set; }
        public string IndexNumber { get; set; }
        public Student Student { get; set; }
        public Subject Subject { get; set; }
        public string Mark { get; set; }
        public DateTime ExamDate { get; set; }
    }
}
