using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskManagerData.Enums;
using TaskManagerData.Models;

namespace TaskManager.Controls
{
    /// <summary>
    /// Логика взаимодействия для StatisticsControl.xaml
    /// </summary>
    public partial class StatisticsControl : UserControl
    {
        public StatisticsControl()
        {
            InitializeComponent();
        }

        public void UpdateStats(List<TaskModel> tasks)
        {
            txtNew.Text =
                $"Новые: {tasks.Count(x => x.Status == TaskProcStatus.New)}";

            txtProgress.Text =
                $"В работе: {tasks.Count(x => x.Status == TaskProcStatus.InProgress)}";

            txtCompleted.Text =
                $"Завершённые: {tasks.Count(x => x.Status == TaskProcStatus.Completed)}";
        }
    }
}
