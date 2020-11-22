using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationGet.Models
{
    public class ExamView
    {
        public int ExamId { get; set; }
        public string IndexNumber { get; set; }
        public Student Student { get; set; }
        public Subject Subject { get; set; }
        public string Mark { get; set; }
        public string ExamDate { get; set; }

        public static implicit operator ExamView(Exam exam)
        {
            if (exam == null)
                return null;
            else
            {
                return new ExamView()
                {
                    ExamId = exam.ExamId,
                    IndexNumber = exam.IndexNumber,
                    Student = exam.Student,
                    Subject = exam.Subject,
                    Mark = exam.Mark,
                    ExamDate = exam.ExamDate.ToString("yyyy-MM-dd")
                };
            }
        }

       

    }
}
