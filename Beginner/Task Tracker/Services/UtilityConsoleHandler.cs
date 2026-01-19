using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Task_Tracker.Helpers;
using Task_Tracker.Models.Dtos;
using Task_Tracker.Models.Enums;
using TaskStatus = Task_Tracker.Models.Enums.TaskStatus;

namespace Task_Tracker.Services
{
    internal static class UtilityConsoleHandler
    {
        public static LineDto ParseAddCommand(string desc)
        {
            string tododesc = desc;
            return new LineDto
            {
                CommandType = Commands.ADD,
                DESC = tododesc
            };
        }
        public static LineDto ParseUpdateCommand(string[] inputs, string desc)
        {
            int id = int.Parse(inputs[1]);
            return new LineDto
            {
                CommandType = Commands.UPDATE,
                DESC = desc,
                Id = id
            };
        }
        public static LineDto ParseDeleteCommand(string[] inputs)
        {
            int id = int.Parse(inputs[1]);
            return new LineDto
            {
                CommandType = Commands.DELETE,
                Id = id
            };
        }
        public static string ReadDescFromInput(string line)
        {
            if(!string.IsNullOrEmpty(line))
            {
                string r = @"\""[^\""]*\""|\\S+";
                var data = Regex.Matches(line, r);
                var res = data.FirstOrDefault()!.Value;
                if (res.StartsWith("\"") && res.EndsWith("\""))
                {
                    res = res.Trim('"');
                }
                return res;
            }
            return string.Empty;
            
        }
        public static int ReadIdFromCommand(string[] inputs)
        {
           return int.Parse(inputs[1]);
        }
        public static TaskStatus? ReadListFilter(string[] inputs)
        {
            if (inputs != null && inputs.Length > 1 && !string.IsNullOrEmpty(inputs[1]))
            {
               return MappingHelper.MapStatusFilter(inputs[1]);
            }
            return null;
        }
    }
}
