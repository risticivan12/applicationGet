using Data.Context;
using Data.DTO;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implements
{
    public class StudentServiceImpl : IStudentService
    {
        private readonly ApplicationDbContext ApplicationDbContext;

        public StudentServiceImpl(ApplicationDbContext applicationDbContext)
        {
            this.ApplicationDbContext = applicationDbContext;
        }


        public async Task<IEnumerable<Student>> GetListOfStudents()
        {
            try
            {
                IEnumerable<Student> students = await ApplicationDbContext.Students.ToListAsync();
                return students.Select(s => s);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
            throw new NotImplementedException();
        }

        public async Task<Student> SaveStudent(Student student)
        {
            try
            {

                Student st = await ApplicationDbContext.Students.FirstOrDefaultAsync(s => s.IndexNumber == student.IndexNumber);
                if (st != null)
                {
                    return null;
                }

                await ApplicationDbContext.Students.AddAsync(student);
                await ApplicationDbContext.SaveChangesAsync();

                return await ApplicationDbContext.Students.FirstOrDefaultAsync(s => s.IndexNumber == student.IndexNumber);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            try
            {

                var StudentUpdated = await this.ApplicationDbContext.Students.
                    FirstOrDefaultAsync(s => s.StudentId == student.StudentId);

                StudentUpdated.IndexNumber = student.IndexNumber;
                StudentUpdated.FirstName = student.FirstName;
                StudentUpdated.LastName = student.LastName;
                StudentUpdated.City = student.City;

                await this.ApplicationDbContext.SaveChangesAsync();
                return StudentUpdated;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<Student> DeleteStudent(Student student)
        {
            try
            {
                ApplicationDbContext.Students.Remove(student);
                await this.ApplicationDbContext.SaveChangesAsync();
                return student;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<IEnumerable<string>> SuggestedStudents(string indexNumber)
        {
            try
            {
                IEnumerable<Student> list = await ApplicationDbContext.Students.Where( s => s.IndexNumber.StartsWith(indexNumber)).ToListAsync();
                if (list == null)
                {
                    return null;
                }
                return  list.Select(s => s.IndexNumber );
            }catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }

        public async Task<Student> GetStudentByIndexNumber(string indexNumber)
        {
            try
            {
                return await ApplicationDbContext.Students.FirstOrDefaultAsync(s => s.IndexNumber == indexNumber);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }


       public async Task<IEnumerable<PassedExam>> GetSubjectsAndMarksByIndexNumber(string indexNumber)
        {

            try
            {
                IEnumerable<PassedExam> list = await ApplicationDbContext.Exams.Join(
                        ApplicationDbContext.Subjects,
                        e => e.Subject.SubjectId,
                        sb => sb.SubjectId,
                        (e, sb) => new { e, sb })
                    .Join(
                        ApplicationDbContext.Students,
                        exam => exam.e.Student.StudentId,
                        st => st.StudentId,
                        (exam, st) => new { exam, st }
                    ).Where(fullEntry => fullEntry.exam.e.IndexNumber == indexNumber)
                    .Select(full => full.exam).Select(print => new PassedExam(){ Title =  print.sb.Title, Mark =print.e.Mark 
                    })
                    .ToListAsync();

                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }
    }
}
