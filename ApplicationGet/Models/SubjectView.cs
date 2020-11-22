using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationGet.Models
{
    public class SubjectView
    {
        public int SubjectId { get; set; }
        public string Title { get; set; }

        public static implicit operator SubjectView(Subject subject)
        {
            if (subject == null)
                return null;
            else
            {
                return new SubjectView()
                {
                    SubjectId = subject.SubjectId,
                    Title = subject.Title
                };
            }
        }
    }
}
