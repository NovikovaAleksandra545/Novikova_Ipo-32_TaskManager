using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TaskManagerData.Models;
using TaskManagerData.Services.Interfaces;

namespace TaskManagerData.Services
{
    public class XmlFileService : IFileService
    {
        public void Save(string path, List<TaskModel> tasks)
        {
            XmlSerializer serializer =
                new XmlSerializer(typeof(List<TaskModel>));

            using FileStream stream = new FileStream(
                path,
                FileMode.Create);

            serializer.Serialize(stream, tasks);
        }

        public List<TaskModel> Load(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Файл не найден.", path);
            }

            XmlSerializer serializer =
                new XmlSerializer(typeof(List<TaskModel>));

            using FileStream stream = new FileStream(
                path,
                FileMode.Open);

            return (List<TaskModel>)serializer.Deserialize(stream)!;
        }
    }
}
