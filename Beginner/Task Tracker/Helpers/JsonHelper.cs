using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Buffers;
using myTask = Task_Tracker.Models.Task;
namespace Task_Tracker.Helpers
{
    public static class JsonHelper
    {
        const string PATH = "data.json";
        private static int ID = 0;
        public static async Task WriteEntityToFile(myTask task)
        {
            try
            {
                List<myTask> tasks = new();

                if (File.Exists(PATH))
                {
                    byte[] rawData = await File.ReadAllBytesAsync(PATH);
                    using var stream = new MemoryStream(rawData);
                    if (rawData != null && rawData.Length > 0)
                    {
                        tasks = await JsonSerializer.DeserializeAsync<List<myTask>>(stream) ?? new List<myTask>();
                    }
                }
                ///[TODO] add or update or remove task here
                myTask newToDo = new()
                {
                    Id = ++ID,
                    Description = task.Description,
                    Status = task.Status,
                    CreatedAt = DateTime.UtcNow
                };
                tasks.Add(newToDo);
                using var fs = new FileStream(PATH, FileMode.Create, FileAccess.Write, FileShare.None);
                using var sw = new Utf8JsonWriter(fs, new JsonWriterOptions { Indented = true });

                await JsonSerializer.SerializeAsync(fs,tasks);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception occurred while writing to file, {ex.Message} ",ex);
            }
        }
        private static async Task<int> GetHighestId()
        {
            //[TODO]
            return 1;
        }
    }
}
