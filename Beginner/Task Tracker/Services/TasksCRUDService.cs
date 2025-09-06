using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myTask = Task_Tracker.Models.Task;
using Task_Tracker.Helpers;

namespace Task_Tracker.Services
{
    internal static class TasksCRUDService
    {
        public static async Task Upsert(myTask task, int? id = null)
        {
            await JsonHelper.UpsertAsync(task, id);
        }
    }
}
