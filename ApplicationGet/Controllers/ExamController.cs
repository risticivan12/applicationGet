using ApplicationGet.Models;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationGet.Controllers
{
    public class ExamController : Controller
    {
        private readonly IExamService ExamService;

        public ExamController(IExamService examService)
        {
            this.ExamService = examService;
        }

        public async Task<IActionResult> Exam()
        {
            IEnumerable<Exam> list = await ExamService.GetListOfExams();
            dynamic model = new ExpandoObject();
            model.List = list;
            return View(model);
        }

        [HttpPost]
        public async Task<Exam> Save([Bind("IndexNumber, SubjectId, Mark, Date")] string indexNumber, int subjectId, string mark, DateTime date)
        {
            return await ExamService.SaveExam(indexNumber, subjectId, mark, date);
        }

        public async Task<Exam> GetExamByIndexNubmerAndSubjectId(string indexNumber, int subjectId)
        {
            return await ExamService.GetExamByIndexNubmerAndSubjectId(indexNumber, subjectId);
        }


        [HttpDelete]
        public async Task<Exam> DeleteExam([Bind("ExamId", "IndexNumber", "SubjectId")] int examId, string indexNumber, int subjectId)
        {
            return await ExamService.DeleteExam(examId, indexNumber, subjectId);
        }

        [HttpPut]
        public async Task<Exam> UpdateExam([Bind("examId", "examMark", "examDate")]int examId, string examMark, DateTime examDate)
        {
            return await ExamService.UpdateExam(examId, examMark, examDate);

        }
    }
}
