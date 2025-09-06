using Task_Tracker.Models.Enums;
using Task_Tracker.Services;
using myTask = Task_Tracker.Models.Task;
using myTaskStatus = Task_Tracker.Models.Enums.TaskStatus;
using Task_Tracker.Helpers;
using System.Text.RegularExpressions;

namespace Task_Tracker
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                //Read args - create
                string appname = AppDomain.CurrentDomain.FriendlyName;
                Console.WriteLine("Welcome to our to-do list app!");
                Console.WriteLine("Press Esc to terminate....");
                while (true)
                {
                    Console.Write(appname + '>');
                    var line = Console.ReadLine();
                    var inputs = line?.Split(" ");
                    if (line == null || inputs == null || inputs.Length == 0)
                    {
                        Console.WriteLine("Please enter valid data!");
                        break;
                    }
                    // Extract info from input
                    string desc = UtilityConsoleHandler.ReadDescFromInput(line);
                    var command = inputs[0];
                    Enum commandEnum = MappingHelper.MapCommand(command);

                    switch (commandEnum)
                    {
                        case Commands.ADD:
                            var addParseResponse = UtilityConsoleHandler.ParseAddCommand(desc);
                            await UtilityCRUD.Create(addParseResponse.DESC);
                            break;
                        case Commands.UPDATE:
                            var updateParseResponse = UtilityConsoleHandler.ParseUpdateCommand(inputs, desc);
                            await UtilityCRUD.Update(updateParseResponse.DESC, updateParseResponse.Id);
                            break;
                        case Commands.LIST:
                            //[TODO]
                            break;
                        case Commands.DELETE:
                            //[TODO]
                            break;
                        case Commands.MARKINPROGRESS:
                            //[TODO]
                            break;
                        case Commands.MARKDONE:
                            //[TODO]
                            break;
                        default:
                            return;
                    }
                }
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            
            ///[TODO] 1- Add Parsing function maybe in utility crud
            ///2- Enable parsing commands on add, delete and update correctly 
            ///3- colorize commands
            ///4- accept todos with spaces 
            
        }
    }
}