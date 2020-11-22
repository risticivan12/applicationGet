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
    public interface IStudentService
    {
        Task<Student> SaveStudent(Student student);
        Task<IEnumerable<Student>> GetListOfStudents();
        Task<Student> UpdateStudent(Student student);
        Task<Student> DeleteStudent(Student student);
        Task<IEnumerable<string>> SuggestedStudents(string indexNumber);
        Task<Student> GetStudentByIndexNumber(string indexNumber);
        Task<IEnumerable<PassedExam>> GetSubjectsAndMarksByIndexNumber(string indexNumber);
    }
}
