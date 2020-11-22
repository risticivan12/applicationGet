using Data.Context;
using Data.DTO;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Service.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Service.Implements
{
    public class SubjectServiceImpl : ISubjectService
    {
        private readonly ApplicationDbContext ApplicationDbContext;

        public SubjectServiceImpl(ApplicationDbContext applicationDbContext)
        {
            this.ApplicationDbContext = applicationDbContext;
        }

        public object IQueryable { get; private set; }

        public async Task<IEnumerable<Subject>> GetListOfSubjects()
        {
            try
            {
                IEnumerable<Subject> listSubjects = await this.ApplicationDbContext.Subjects.ToListAsync();
                return listSubjects;
            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<Subject> Save(Subject subject)
        {
            try
            {
                Subject sb = await ApplicationDbContext.Subjects.FirstOrDefaultAsync(s => s.Title == subject.Title);
                if (sb != null)
                {
                    return null;
                }

                ApplicationDbContext.Subjects.Add(subject);
                await this.ApplicationDbContext.SaveChangesAsync();
                return await ApplicationDbContext.Subjects.FirstOrDefaultAsync(s => s.Title == subject.Title);
            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }


        public async Task<Subject> DeleteSubject(Subject subject)
        {
            try
            {
               ApplicationDbContext.Subjects.Remove(subject);
               await this.ApplicationDbContext.SaveChangesAsync();
               return subject;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<Subject> UpdateSubject(Subject subject)
        {
            try
            {
                var SubjectUpdate = await ApplicationDbContext.Subjects.FirstOrDefaultAsync(s => s.SubjectId == subject.SubjectId);

                SubjectUpdate.Title = subject.Title;


                 await this.ApplicationDbContext.SaveChangesAsync();


                return SubjectUpdate;

            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Subject>> GetListOfSubjectsByIndexNumber(string indexNumber)
        {
            try
            {

                IEnumerable<Subject> subjectsByIndex = await ApplicationDbContext.Exams.Join(
                        ApplicationDbContext.Subjects,
                        e => e.Subject.SubjectId,
                        s => s.SubjectId,
                        (e, s) => new { e, s }
                    ).Where(fullEntry => fullEntry.e.IndexNumber == indexNumber).
                    Select(fullEntry => fullEntry.s).ToListAsync();

                IEnumerable<Subject> subjectsAll = await ApplicationDbContext.Subjects.ToListAsync();

                return subjectsAll.Except(subjectsByIndex).ToList();


            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }


        public async Task<IEnumerable<SubjectStudents>> GetListOfStudentsBySubjectId(int subjectId)
        {
            try
            {
                IEnumerable <SubjectStudents> list  = await ApplicationDbContext.Exams.Join(
                        ApplicationDbContext.Subjects,
                        e => e.Subject.SubjectId,
                        sb => sb.SubjectId,
                        (e, sb) => new { e, sb }).Where(fullEntry => fullEntry.sb.SubjectId == subjectId)
                    .Join(
                        ApplicationDbContext.Students,
                        exam => exam.e.Student.StudentId,
                        st => st.StudentId,
                        (exam, st) => new { exam, st }
                    ).Select(entries => new SubjectStudents{
                        StudentId = entries.st.StudentId,
                        IndexNumber =  entries.st.IndexNumber,
                        FirstName = entries.st.FirstName,
                        LastName = entries.st.LastName,
                        City = entries.st.City,
                        ExamDate = entries.exam.e.ExamDate,
                        Mark = entries.exam.e.Mark
                        }).ToListAsync();

                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
    }
}
