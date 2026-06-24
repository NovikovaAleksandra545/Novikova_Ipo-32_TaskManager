using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerData.Enums;
using TaskManagerData.Models;

namespace TaskManagerData.Services.Interfaces
{
    public interface ITaskService
    {
        bool Add(TaskModel task);

        bool Update(TaskModel task);

        bool Delete(TaskModel task);

        List<TaskModel> GetAllTasks();

        List<TaskModel> GetTasksByStatus(TaskProcStatus status);

        List<TaskModel> Search(string text);
    }
}
