using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerData.Context;
using TaskManagerData.Enums;
using TaskManagerData.Models;
using TaskManagerData.Services.Interfaces;

namespace TaskManagerData.Services
{
    
    public class TaskService : ITaskService
    {
        private readonly ApplicationContext _context;

        public TaskService(ApplicationContext context)
        {
            _context = context;
        }

        public bool Add(TaskModel task)
        {
            bool result = false;
            if (task != null)
            {
                if (!String.IsNullOrWhiteSpace(task.Title) 
                    && task.DueDate > DateTime.Now)
                {
                    _context.Tasks.Add(task);
                    result = true;
                }
            }
            return result;
        }

        public List<TaskModel> GetAllTasks()
        {
            return _context.Tasks;
        }

        public List<TaskModel> GetTasksByStatus(TaskProcStatus status)
        {
            return _context.Tasks
                           .Where(t => t.Status == status)
                           .ToList();
        }

        public List<TaskModel> Search(string text)
        {
            return _context.Tasks
                           .Where(t =>
                               t.Title.Contains(text,
                                   StringComparison.OrdinalIgnoreCase)
                               ||
                               t.Description.Contains(text,
                                   StringComparison.OrdinalIgnoreCase))
                           .ToList();
        }

        public bool Update(TaskModel task)
        {
            bool result = false;
            

            if (task != null)
            {
                if (!String.IsNullOrWhiteSpace(task.Title))
                {
                    var target = _context.Tasks.FirstOrDefault(x => x.Id == task.Id);
                    if (target != null)
                    {
                        if (target.DueDate <= task.DueDate)
                        {
                            int id = _context.Tasks.IndexOf(target);
                            if (id != -1) _context.Tasks[id] = task;

                            result = true;
                        }
                    }
                }   
            }
            return result;
        }

        public bool Delete(TaskModel task)
        {
            bool result = false;
            if (task != null)
            {
                if (_context.Tasks.Contains(task))
                {
                    _context.Tasks.Remove(task);
                    result = true;
                }
            }
            return result;
        } 
    }
}
