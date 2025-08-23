using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myTaskStatus = Task_Tracker.Models.Enums.TaskStatus;

namespace Task_Tracker.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public myTaskStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
