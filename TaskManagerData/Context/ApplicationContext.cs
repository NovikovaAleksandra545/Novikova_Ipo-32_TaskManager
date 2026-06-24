using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerData.Enums;
using TaskManagerData.Models;

namespace TaskManagerData.Context
{
    

    public class ApplicationContext
    {
        public List<TaskModel> Tasks;

        public ApplicationContext()
        {
            Tasks = new List<TaskModel>();
            

            DataFill();
        }

        public void DataFill()
        {
            Tasks.Add(new TaskModel
            {
                Title = "Подготовить отчёт",
                Description = "Отчет по проекту",
                Priority = TaskPriority.High,
                DueDate = DateTime.Now.AddDays(2),
                Status = TaskProcStatus.InProgress
            });

            Tasks.Add(new TaskModel
            {
                Title = "Проверить почту",
                Description = "Ответить на письма",
                Priority = TaskPriority.Medium,
                DueDate = DateTime.Now.AddDays(1),
                Status = TaskProcStatus.New
            });

            Tasks.Add(new TaskModel
            {
                Title = "Обновить документацию",
                Description = "Добавить описание",
                Priority = TaskPriority.Low,
                DueDate = DateTime.Now.AddDays(5),
                Status = TaskProcStatus.Completed
            });

            Tasks.Add(new TaskModel
            {
                Title = "Исправить ошибку",
                Description = "Проверить неверный код",
                Priority = TaskPriority.High,
                DueDate = DateTime.Now.AddDays(3),
                Status = TaskProcStatus.InProgress
            });

            Tasks.Add(new TaskModel
            {
                Title = "Провести тестирование",
                Description = "Проверить работу программы",
                Priority = TaskPriority.High,
                DueDate = DateTime.Now.AddDays(6),
                Status = TaskProcStatus.Completed
            });
        }
    }
}
