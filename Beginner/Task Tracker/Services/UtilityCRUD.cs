using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using systemTask =  System.Threading.Tasks.Task;
using Task_Tracker.Models.Enums;
using Task_Tracker.Models;
using myTask = Task_Tracker.Models.Task;
using myTaskStatus = Task_Tracker.Models.Enums.TaskStatus;
using TaskStatus = Task_Tracker.Models.Enums.TaskStatus;

namespace Task_Tracker.Services
{
    public static class UtilityCRUD
    {
        public static async systemTask Create(string newTask)
        {
            myTask newtodo = new()
            {
                Description = newTask,
                Status = myTaskStatus.TODO,
            };
            await TasksCRUDService.Upsert(newtodo);
        }
        public static async systemTask Update(string desc, int id)
        {
            myTask tobeupdated = new()
            {
                Description = desc,
                Id = id
            };
            await TasksCRUDService.Upsert(tobeupdated, tobeupdated.Id);
        }
        public static async systemTask Delete(int id)
        {
            await TasksCRUDService.Delete(id);
        }
        public static async systemTask List(myTaskStatus? filter)
        {
            await TasksCRUDService.ListAsync(filter);
        }
        public static async systemTask UpdateStatus(int id,  TaskStatus status)
        {
            await TasksCRUDService.UpdateStatus(status, id);
        }
    }
}
