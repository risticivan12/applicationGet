using ApplicationGet.Models;
using Data.DTO;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectService subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            this.subjectService = subjectService;
        }

        public async Task<IActionResult> Subject()
        {
            IEnumerable<Subject> listOfSubjects = await this.subjectService.GetListOfSubjects();

            dynamic model = new ExpandoObject();
            model.List = listOfSubjects;

            return View(model);
        }

        [HttpPost]
        public async Task<Subject> Save([Bind("Title")] Subject subject)
        {
            return await this.subjectService.Save(subject);

        }

        [HttpDelete]
        public async Task<Subject> Delete([Bind("SubjectId, Title")] Subject subject)
        {
            return await subjectService.DeleteSubject(subject);
        }

        [HttpPut]
        public async Task<Subject> Update([Bind("SubjectId, Title")] Subject subject)
        {
            return await subjectService.UpdateSubject(subject);
        }

        [HttpPut]
        public async Task<IEnumerable<Subject>> List([Bind("IndexNumber")]string indexNumber){
            return await subjectService.GetListOfSubjectsByIndexNumber(indexNumber);
        }

        [HttpPut]
        public async Task<IEnumerable<SubjectStudents>> GetListOfStudentsBySubjectId([Bind("SubjectId")]int subjectId)
        {
            return await subjectService.GetListOfStudentsBySubjectId(subjectId);
        }

    }
}
