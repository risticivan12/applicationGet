using Data.DTO;
using Data.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ISubjectService
    {
        Task<Subject> Save(Subject subject);
        Task<IEnumerable<Subject>> GetListOfSubjects();
        Task<Subject> DeleteSubject(Subject subject);
        Task<Subject> UpdateSubject(Subject subject);
        Task<IEnumerable<Subject>> GetListOfSubjectsByIndexNumber(string indexNumber);
        Task<IEnumerable<SubjectStudents>> GetListOfStudentsBySubjectId(int subjectId);
    }
}
