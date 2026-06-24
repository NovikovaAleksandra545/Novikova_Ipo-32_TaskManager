using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerData.Models;

namespace TaskManagerData.Services.Interfaces
{
    public interface IFileService
    {
        void Save(string path, List<TaskModel> tasks);

        List<TaskModel> Load(string path);
    }
}
