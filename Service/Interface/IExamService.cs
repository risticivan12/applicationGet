using Data.Context;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IExamService
    {
        Task<Exam> SaveExam(string indexNumber, int subjectId, string mark, DateTime date);
        Task<IEnumerable<Exam>> GetListOfExams();
        Task<Exam> GetExamByIndexNubmerAndSubjectId(string indexNumber, int subjectId);
        Task<Exam> DeleteExam(int examId, string indexNumber, int subjectId);
        Task<Exam> UpdateExam(int examId, string mark, DateTime date);
    }
}
