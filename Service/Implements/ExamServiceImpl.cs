using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Implements
{
    public class ExamServiceImpl : IExamService
    {
        private readonly ApplicationDbContext ApplicationDbContext;

        public ExamServiceImpl(ApplicationDbContext applicationDbContext)
        {
            this.ApplicationDbContext = applicationDbContext;
        }

        public async Task<Exam> DeleteExam(int examId, string indexNumber, int subjectId)
        {
             try
            {
                Student student = await ApplicationDbContext.Students.FirstOrDefaultAsync(s => s.IndexNumber == indexNumber);
                Subject subject = await ApplicationDbContext.Subjects.FirstOrDefaultAsync(sb => sb.SubjectId == subjectId);
                Exam exam = await ApplicationDbContext.Exams.FirstOrDefaultAsync(e => e.ExamId == examId);
                exam.Student = student;
                exam.Subject = subject;
                ApplicationDbContext.Exams.Remove(exam);
                await ApplicationDbContext.SaveChangesAsync();
                return exam;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<Exam> GetExamByIndexNubmerAndSubjectId(string indexNumber, int subjectId)
        {
            try
            {
                return await ApplicationDbContext.Exams.FirstOrDefaultAsync(e => e.IndexNumber == indexNumber && e.Subject.SubjectId == subjectId);
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<IEnumerable<Exam>> GetListOfExams()
        {
            try
            {
                   
                IEnumerable<Exam> exams =  await ApplicationDbContext.Exams
                  .Join(
                      ApplicationDbContext.Subjects,
                      e => e.Subject.SubjectId,
                      sb => sb.SubjectId,
                      (e, sb) => new {e, sb }
                   ).Join(
                          ApplicationDbContext.Students,
                          result => result.e.Student.StudentId,
                          st => st.StudentId,
                          (result, st) => new Exam{
                              ExamId = result.e.ExamId,
                              ExamDate = result.e.ExamDate,
                              IndexNumber = result.e.IndexNumber,
                              Mark = result.e.Mark,
                              Subject = result.sb,
                              Student = st
                          }
                       )
                .ToListAsync();


                return exams;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public async Task<Exam> SaveExam(string indexNumber, int subjectId, string mark, DateTime date)
        {
            
            try
            {
                Student student = await ApplicationDbContext.Students.FirstOrDefaultAsync(s => s.IndexNumber == indexNumber);
                Subject subject = await ApplicationDbContext.Subjects.FirstOrDefaultAsync(sb => sb.SubjectId == subjectId);
                Exam exam = new Exam {
                    IndexNumber = indexNumber, 
                    Student = student,
                    Subject = subject,
                    ExamDate = date,
                    Mark = mark
                
                };

                 await ApplicationDbContext.AddAsync(exam);
                 await ApplicationDbContext.SaveChangesAsync();
                return await ApplicationDbContext.Exams.FirstOrDefaultAsync(e => e.IndexNumber == indexNumber && e.Subject.SubjectId == subjectId);
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<Exam> UpdateExam(int examId, string mark, DateTime date)
        {
            try
            {
                Exam ExamUpdated = await ApplicationDbContext.Exams.FirstOrDefaultAsync(e => e.ExamId == examId);

                ExamUpdated.Mark = mark;
                ExamUpdated.ExamDate = date;

                await this.ApplicationDbContext.SaveChangesAsync();

                return ExamUpdated;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
    }
}
