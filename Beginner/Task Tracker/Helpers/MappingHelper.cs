using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Tracker.Models.Enums;

namespace Task_Tracker.Helpers
{
    public static class MappingHelper
    {
        public static Dictionary<string, Enum> CommandMapper = new Dictionary<string, Enum>
        {
            {"add",Commands.ADD },
            {"update",Commands.UPDATE },
            {"delete",Commands.DELETE },
            {"list",Commands.LIST },
            {"mark-done",Commands.MARKDONE },
            {"mark-in-progress",Commands.MARKINPROGRESS },
            {"close",Commands.QUIT }
        };
        public static Enum MapCommand(string command)
        {
            return CommandMapper[command];
        }

    }
}
