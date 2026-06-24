using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerData.Enums;
using TaskManagerData.Models;
using TaskManagerData.Services;

namespace TaskManagerData.Services.Tests
{
    [TestClass()]
    public class XmlFileServiceTests
    {
        [TestMethod()]
        public void PathTest()
        {
            XmlFileService svc = new XmlFileService();
            string _path = "test_tasks.xml";

            Assert.IsNotNull(svc);
            Assert.IsInstanceOfType(svc, typeof(XmlFileService));
            Assert.IsTrue(File.Exists(_path));
        }

        [TestMethod()]
        public void SaveTest()
        {
            XmlFileService svc = new XmlFileService();
            string _path = "test_tasks.xml";

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
            XmlFileService svc = new XmlFileService();
            string _path = "test_tasks.xml";
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