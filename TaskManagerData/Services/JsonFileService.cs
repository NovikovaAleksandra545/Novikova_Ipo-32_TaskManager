using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManagerData.Models;
using TaskManagerData.Services.Interfaces;

namespace TaskManagerData.Services
{
    public class JsonFileService : IFileService
    {
        public void Save(string path, List<TaskModel> tasks)
        {
            string json = JsonSerializer.Serialize(
                tasks,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(path, json);
        }

        public List<TaskModel> Load(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Файл не найден.", path);
            }

            string json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<List<TaskModel>>(json)
                   ?? new List<TaskModel>();
        }
    }
}
