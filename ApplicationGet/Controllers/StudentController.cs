using Data.DTO;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationGet.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public async Task<IActionResult> Student()
        {
            IEnumerable<Student> list =  await studentService.GetListOfStudents();
            dynamic model = new ExpandoObject();
            model.List = list;
            return  View(model);
        }

        [HttpPost]
        public async Task<Student> Save([Bind("IndexNumber","FirstName", "LastName", "City")]Student student)
        {
            return await studentService.SaveStudent(student);
        }

        public async Task<Student> Update([Bind("StudentId","IndexNumber", "FirstName", "LastName", "City")] Student student)
        {
            return await studentService.UpdateStudent(student);
        }

        [HttpDelete]
        public async Task<Student> Delete([Bind("StudentId", "IndexNumber", "FirstName", "LastName", "City")] Student student)
        {
            return await studentService.DeleteStudent(student);
        }

        [HttpPut]
        public async Task<IEnumerable<string>> Suggested([Bind("IndexNumber")]string indexNumber)
        {
            return await studentService.SuggestedStudents(indexNumber);
        }

        [HttpPut]
        public async Task<Student> GetStudent([Bind("IndexNubmer")]string indexNumber)
        {
            return await studentService.GetStudentByIndexNumber(indexNumber);
        }

        [HttpPut]
        public async Task<IActionResult> GetSubjectsAndMarksByIndexNumber(string indexNumber)
        {
            IEnumerable<PassedExam> list = await studentService.GetSubjectsAndMarksByIndexNumber(indexNumber);
            return PartialView("~/Views/Student/ExamsPartialView.cshtml", list);
        }
    }
}
