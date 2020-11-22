using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        public string Title { get; set; }


        public override bool Equals(object obj)
        {
            return obj is Subject subject &&
                   SubjectId == subject.SubjectId &&
                   Title == subject.Title;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SubjectId, Title);
        }
    }
}
