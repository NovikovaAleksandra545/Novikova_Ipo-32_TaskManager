using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
    public class TaskServiceTests
    {
        [TestMethod()]
        public void TaskServiceTest()
        {
            ApplicationContext context = new ApplicationContext();
            TaskService svc = new TaskService(context);

            Assert.IsNotNull(svc);
            Assert.IsInstanceOfType(svc, typeof(TaskService));
        }

        [TestMethod()]
        public void AddTest()
        {
            ApplicationContext context = new ApplicationContext();
            TaskService svc = new TaskService(context);

            var task = new TaskModel
            {
                Title = "Задание 1",
                Description = "Описание",
                DueDate = DateTime.Now.AddDays(1),
                Priority = TaskPriority.High
            };

            bool result = svc.Add(task);

            Assert.IsNotNull(svc);
            Assert.IsTrue(result);
            Assert.AreEqual(1, context.Tasks.Count);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            ApplicationContext context = new ApplicationContext();
            TaskService svc = new TaskService(context);
            var task = new TaskModel
            {
                Title = "Задание1",
                DueDate = DateTime.Now.AddDays(1)
            };

            context.Tasks.Add(task);

            var updatedTask = new TaskModel
            {
                Id = task.Id,
                Title = "Задание 1.1",
                DueDate = DateTime.Now.AddDays(2)
            };

            bool result = svc.Update(updatedTask);

            Assert.IsTrue(result);
            Assert.AreEqual("Задание 1.1", context.Tasks[0].Title);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            ApplicationContext context = new ApplicationContext();
            TaskService svc = new TaskService(context);
            var task = new TaskModel
            {
                Title = "Задание 1",
                DueDate = DateTime.Now.AddDays(1)
            };

            context.Tasks.Add(task);

            bool result = svc.Delete(task);

            Assert.IsTrue(result);
            Assert.AreEqual(0, context.Tasks.Count);
        }

        [TestMethod()]
        public void GetTasksByStatusTest()
        {
            ApplicationContext context = new ApplicationContext();
            TaskService svc = new TaskService(context);
            context.Tasks.Add(new TaskModel
            {
                Title = "Написание Отчета",
                DueDate = DateTime.Now.AddDays(1),
                Status = TaskProcStatus.New
            });

            context.Tasks.Add(new TaskModel
            {
                Title = "Тестирование Функций",
                DueDate = DateTime.Now.AddDays(1),
                Status = TaskProcStatus.Completed
            });

            var result = svc.GetTasksByStatus(TaskProcStatus.New);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(TaskProcStatus.New, result[0].Status);
        }

        [TestMethod()]
        public void SearchTest()
        {
            ApplicationContext context = new ApplicationContext();
            TaskService svc = new TaskService(context);
            context.Tasks.Add(new TaskModel
            {
                Title = "Написание Отчета",
                Description = "Подробный отчет",
                DueDate = DateTime.Now.AddDays(1)
            });

            var result = svc.Search("отчет");

            Assert.AreEqual(1, result.Count);
        }
    }
}