using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myTask = Task_Tracker.Models.Task;
using Task_Tracker.Helpers;
using Task_Tracker.Models.Enums;
using TaskStatus = Task_Tracker.Models.Enums.TaskStatus;

namespace Task_Tracker.Services
{
    internal static class TasksCRUDService
    {
        public static async Task Upsert(myTask task, int? id = null)
        {
            await JsonHelper.UpsertAsync(task, id);
        }
        public static async Task Delete(int id)
        {
            await JsonHelper.DeleteAsync(id);
        }
        public static async Task ListAsync(TaskStatus? filter)
        {
            await JsonHelper.ListAsync(filter);
        }
        public static async Task UpdateStatus(TaskStatus status, int id)
        {
            await JsonHelper.UpdateStatusAsync(status, id);
        }
    }
}
