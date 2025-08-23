using Task_Tracker.Services;
using myTask = Task_Tracker.Models.Task;
using myTaskStatus = Task_Tracker.Models.Enums.TaskStatus;

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
                Console.Write(appname + '>');
                var line = Console.ReadLine();
                var command = line.Split(" ")[0];
                var tododesc = line.Split(" ")[1];
                myTask newTODO = new()
                {
                    Description = tododesc,
                    Status = myTaskStatus.TODO,
                };
                await TasksCRUDService.AddTask(newTODO);
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}