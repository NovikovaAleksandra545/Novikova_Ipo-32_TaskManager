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
using System.Windows.Shapes;
using TaskManagerData.Enums;
using TaskManagerData.Models;

namespace TaskManager.Views
{
    /// <summary>
    /// Логика взаимодействия для TaskModalWindow.xaml
    /// </summary>
    public partial class TaskModalWindow : Window
    {
        public TaskModel Task { get; private set; }

        public TaskModalWindow(TaskModel task = null)
        {
            InitializeComponent();

            cbPriority.ItemsSource = Enum.GetValues(typeof(TaskPriority));
            cbStatus.ItemsSource = Enum.GetValues(typeof(TaskProcStatus));

            if (task == null)
            {
                Task = new TaskModel();
                dpDate.SelectedDate = DateTime.Now.AddDays(1);
            }
            else
            {
                Task = task;

                tbTitle.Text = task.Title;
                tbDescription.Text = task.Description;
                cbPriority.SelectedItem = task.Priority;
                cbStatus.SelectedItem = task.Status;
                dpDate.SelectedDate = task.DueDate;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbTitle.Text) ||
                string.IsNullOrWhiteSpace(tbDescription.Text) ||
                cbPriority.SelectedItem == null ||
                cbStatus.SelectedItem == null ||
                dpDate.SelectedDate == null)
            {
                MessageBox.Show(
                    "Заполните все поля.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            Task.Title = tbTitle.Text;
            Task.Description = tbDescription.Text;
            Task.Priority = (TaskPriority)cbPriority.SelectedItem;
            Task.Status = (TaskProcStatus)cbStatus.SelectedItem;
            Task.DueDate = dpDate.SelectedDate.Value;

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
