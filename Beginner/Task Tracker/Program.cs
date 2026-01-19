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
                Console.WriteLine("Valid commands are as follows: ");
                Console.WriteLine("add \"new to-do!\"");
                Console.WriteLine("update {id}");
                Console.WriteLine("delete {id}");
                Console.WriteLine("list");
                Console.WriteLine("list {status filter, ex: done, todo, in-progress}");
                Console.WriteLine("mark-in-progress {id}");
                Console.WriteLine("mark-done {id}");
                Console.WriteLine("close");

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
                    try
                    {
                        var command = inputs[0];
                        Enum commandEnum = MappingHelper.MapCommand(command);

                        switch (commandEnum)
                        {
                            case Commands.ADD:
                                string addtask = UtilityConsoleHandler.ReadDescFromInput(line);
                                var addParseResponse = UtilityConsoleHandler.ParseAddCommand(addtask);
                                await UtilityCRUD.Create(addParseResponse.DESC);
                                break;
                            case Commands.UPDATE:
                                string desc = UtilityConsoleHandler.ReadDescFromInput(line);
                                var updateParseResponse = UtilityConsoleHandler.ParseUpdateCommand(inputs, desc);
                                await UtilityCRUD.Update(updateParseResponse.DESC, updateParseResponse.Id);
                                break;
                            case Commands.LIST:
                                myTaskStatus? listFilter = UtilityConsoleHandler.ReadListFilter(inputs);
                                await UtilityCRUD.List(listFilter);
                                break;
                            case Commands.DELETE:
                                var deleteParseResponse = UtilityConsoleHandler.ParseDeleteCommand(inputs);
                                await UtilityCRUD.Delete(deleteParseResponse.Id);
                                break;
                            case Commands.MARKINPROGRESS:
                                var id = UtilityConsoleHandler.ReadIdFromCommand(inputs);
                                await UtilityCRUD.UpdateStatus(id, myTaskStatus.INPROGRESS);
                                break;
                            case Commands.MARKDONE:
                                var todoId = UtilityConsoleHandler.ReadIdFromCommand(inputs);
                                await UtilityCRUD.UpdateStatus(todoId, myTaskStatus.DONE);
                                break;
                            case Commands.QUIT:
                                return;
                            default:
                                return;
                        }
                    
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception ocurred when trying to parse inputs, please enter a valid command format!");
                    }
                }
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }
}