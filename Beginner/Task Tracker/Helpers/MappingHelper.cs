using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Tracker.Models.Enums;
using TaskStatus = Task_Tracker.Models.Enums.TaskStatus;

namespace Task_Tracker.Helpers
{
    public static class MappingHelper
    {
        private static Dictionary<string, Enum> CommandMapper = new Dictionary<string, Enum>
        {
            {"add",Commands.ADD },
            {"update",Commands.UPDATE },
            {"delete",Commands.DELETE },
            {"list",Commands.LIST },
            {"mark-done",Commands.MARKDONE },
            {"mark-in-progress",Commands.MARKINPROGRESS },
            {"close",Commands.QUIT }
        };
        private static Dictionary<string, TaskStatus> StatusMapper = new Dictionary<string, TaskStatus>
        {
            {"done",TaskStatus.DONE },
            {"todo",TaskStatus.TODO },
            {"in-progress",TaskStatus.INPROGRESS }
        };
        
        public static Enum MapCommand(string command)
        {
            return CommandMapper[command];
        }
        public static TaskStatus MapStatusFilter(string status)
        {
            return StatusMapper[status];
        }
        public static string GetKeyName(TaskStatus status)
        {
            return StatusMapper.Keys.FirstOrDefault(k => StatusMapper[k] == status);
        }

    }
}
