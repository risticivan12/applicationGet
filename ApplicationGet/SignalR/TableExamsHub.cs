using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationGet.Controllers;
using Data.Entities;
using Microsoft.AspNetCore.SignalR;

namespace ApplicationGet.signalR
{
    public class TableExamsHub : Hub
    {
    
        public async Task UpdateTable (Exam exam) {

          //  Exam exam = await ExamController.GetExamByIndexNubmerAndSubjectId(indexNumber, subjectId);
            await Clients.All.SendAsync("UpdatedData", exam);
        }
    }
}
