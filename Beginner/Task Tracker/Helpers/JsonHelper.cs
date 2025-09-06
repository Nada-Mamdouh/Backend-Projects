using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using systemTask = System.Threading.Tasks.Task;
using System.Text.Json;
using System.Buffers;
using myTask = Task_Tracker.Models.Task;
using System.Reflection;
using System.IO;
using Task_Tracker.Models;

namespace Task_Tracker.Helpers
{
    public static class JsonHelper
    {
        public static readonly string PATH = Path.Combine(AppContext.BaseDirectory, @"..\..\..\data.json");
        private static int Id = 0;
        public static async systemTask UpsertAsync(myTask task,int? id)
        {
            try
            {
                // 1- Read data from json file & deserialize
                List<myTask> tasks = await ReadFromFileAsync();
                int highestId = 0;

                // 2- add or update here 
                if(id == null || id == 0) //Create
                {
                    int lastId = await GetHighestId();
                    highestId = lastId + 1;
                    myTask newToDo = new()
                    {
                        Id = highestId,
                        Description = task.Description,
                        Status = task.Status,
                        CreatedAt = DateTime.UtcNow
                    };
                    tasks.Add(newToDo);
                }
                else if(id != null && id > 0) //Update
                {
                    var tasksList = await ReadFromFileAsync();
                    var obj = tasksList.FirstOrDefault(o => o.Id == id);
                    if(obj != null)
                    {
                        obj.Description = task.Description;
                        obj.Status = task.Status;
                        obj.UpdatedAt = DateTime.UtcNow;
                        tasks.Add(obj);
                    }
                }

                // 3- write again to json file
                await WriteToJsonFileAsync(tasks);
                Console.WriteLine($"Task added successfully (ID: {highestId})");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception occurred while writing to file, {ex.Message} ",ex);
            }
        }
        private static async Task<int> GetHighestId()
        { 
            var tasks = await ReadFromFileAsync();
            int highestId = tasks.LastOrDefault()?.Id ?? 0;
            return highestId;
        }
        private static async Task<List<myTask>> ReadFromFileAsync()
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
            return tasks;
        }
        private static async systemTask WriteToJsonFileAsync(List<myTask> tasks)
        {
            using var fs = new FileStream(PATH, FileMode.Create, FileAccess.Write, FileShare.None);
            using var sw = new Utf8JsonWriter(fs, new JsonWriterOptions { Indented = true });

            await JsonSerializer.SerializeAsync(fs, tasks);
        }
    }
}
