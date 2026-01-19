using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using systemTask = System.Threading.Tasks.Task;
using System.Text.Json;
using System.Buffers;
using myTask = Task_Tracker.Models.Task;
using enums = Task_Tracker.Models.Enums;
using System.Reflection;
using System.IO;
using Task_Tracker.Models;
using System.ComponentModel;
using System.Threading.Tasks;
using Task_Tracker.Models.Enums;

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
                    highestId = ++lastId;
                    myTask newToDo = new()
                    {
                        Id = highestId,
                        Description = task.Description,
                        Status = task.Status,
                        CreatedAt = DateTime.UtcNow
                    };
                    tasks.Add(newToDo);
                    Console.WriteLine($"Task added successfully (ID: {highestId})");
                }
                else if(id != null && id > 0) //Update
                {
                    var obj = tasks.FirstOrDefault(o => o.Id == id);
                    if(obj != null)
                    {
                        obj.Description = task.Description;
                        obj.Status = task.Status;
                        obj.UpdatedAt = DateTime.UtcNow;
                        Console.WriteLine($"Task with ID: {obj?.Id} updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine($"No to-do with ID: {id} was found!");
                    }
                    
                }

                // 3- write again to json file
                await WriteToJsonFileAsync(tasks);
                
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception occurred while writing to file, {ex.Message} ",ex);
            }
        }
        public static async systemTask DeleteAsync(int id)
        {
            try
            {
                var todos = await ReadFromFileAsync();
                var todoToBeDeleted = todos.FirstOrDefault(t => t.Id == id);
                if (todoToBeDeleted == null) Console.WriteLine("To do was not found!");
                else
                {
                    todos.Remove(todoToBeDeleted);
                    await WriteToJsonFileAsync(todos);
                    Console.WriteLine($"Todo with Id: {todoToBeDeleted.Id} was deleted successfully!");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception occurred while writing storing todo, {ex.Message}");
            }
        }

        public static async systemTask ListAsync(enums.TaskStatus? filter)
        {
            try
            {
                var todos = await ReadFromFileAsync();
                
                if(todos != null)
                {
                    if (filter != null) todos = todos.Where(t => t.Status == filter).ToList();
                    foreach (var todo in todos)
                    {
                        Console.WriteLine($"{todo.Id}\t{MappingHelper.GetKeyName(todo.Status)}\t{todo.Description}");
                    }
                }
                else
                {
                    Console.WriteLine("You have no to-dos, you may add a new one!");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred while listing todos");
            }
        }
        public static async systemTask UpdateStatusAsync(enums.TaskStatus status, int id)
        {
            try
            {
                List<myTask> tasks = await ReadFromFileAsync();
                if (id > 0) //Update
                {
                    var obj = tasks.FirstOrDefault(o => o.Id == id);
                    if (obj != null)
                    {
                        obj.Status = status;
                        obj.UpdatedAt = DateTime.UtcNow;
                        Console.WriteLine($"Task with ID: {obj?.Id} status was updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine($"No to-do with ID: {id} was found!");
                    }

                }

                // 3- write again to json file
                await WriteToJsonFileAsync(tasks);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred while updating to-do status, {ex.Message} ", ex);
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
