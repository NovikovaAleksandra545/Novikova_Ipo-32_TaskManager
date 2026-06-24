using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerData.Context;
using TaskManagerData.Enums;
using TaskManagerData.Models;
using TaskManagerData.Services;

namespace TaskManagerData.Services.Tests
{
    [TestClass()]
    public class JsonFileServiceTests
    {
        [TestMethod()]
        public void PathTest()
        {
            JsonFileService svc = new JsonFileService();
            string _path = "test_tasks.json";

            Assert.IsNotNull(svc);
            Assert.IsInstanceOfType(svc, typeof(JsonFileService));
            Assert.IsTrue(File.Exists(_path));
        }

        [TestMethod()]
        public void SaveTest()
        {
            JsonFileService svc = new JsonFileService();
            string _path = "test_tasks.json";

            var tasks = new List<TaskModel>
            {
                new TaskModel
                {
                    Title = "Задание 1",
                    Description = "Описание",
                    DueDate = DateTime.Now.AddDays(1),
                    Status = TaskProcStatus.New
                },
                new TaskModel
                {
                    Title = "Задание2",
                    DueDate = DateTime.Now.AddDays(2)
                }
            };

            svc.Save(_path, tasks);

            Assert.IsTrue(File.Exists(_path));
        }

        [TestMethod()]
        public void LoadTest()
        {
            JsonFileService svc = new JsonFileService();
            string _path = "test_tasks.json";
            var tasks = new List<TaskModel>
            {
                new TaskModel
                {
                    Title = "Задание 1",
                    Description = "Описание",
                    DueDate = DateTime.Now.AddDays(1),
                    Status = TaskProcStatus.Completed
                },
                new TaskModel
                {
                    Title = "Задание2",
                    DueDate = DateTime.Now.AddDays(2)
                }
            };

            svc.Save(_path, tasks);

            var loadedTasks = svc.Load(_path);

            Assert.AreEqual(2, loadedTasks.Count);
            Assert.AreEqual("Задание 1", loadedTasks[0].Title);
            Assert.AreEqual("Задание2", loadedTasks[1].Title);
            Assert.AreEqual(TaskProcStatus.Completed,
                            loadedTasks[0].Status);
            Assert.AreEqual(TaskProcStatus.New,
                            loadedTasks[1].Status);
        }
    }
}