using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskManager.Views;
using TaskManagerData.Context;
using TaskManagerData.Enums;
using TaskManagerData.Models;
using TaskManagerData.Services;
using TaskManagerData.Services.Interfaces;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ITaskService _service;

        private readonly IFileService _jsonService =
            new JsonFileService();

        private readonly IFileService _xmlService =
            new XmlFileService();

        public MainWindow()
        {
            InitializeComponent();

            _service = new TaskService(new ApplicationContext());

            cbFilter.ItemsSource =
                Enum.GetValues(typeof(TaskProcStatus));

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            tasksGrid.ItemsSource = null;
            tasksGrid.ItemsSource = _service.GetAllTasks();

            stats.UpdateStats(_service.GetAllTasks());
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var window = new TaskModalWindow();

            if (window.ShowDialog() == true)
            {
                if (_service.Add(window.Task))
                    RefreshGrid();
                else
                    MessageBox.Show("Некорректные данные");
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var task = tasksGrid.SelectedItem as TaskModel;

            if (task == null)
                return;

            var copy = new TaskModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                Status = task.Status,
                DueDate = task.DueDate
            };

            var window = new TaskModalWindow(copy);

            if (window.ShowDialog() == true)
            {
                if (_service.Update(window.Task))
                    RefreshGrid();
                else
                    MessageBox.Show("Изменение невозможно");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var task = tasksGrid.SelectedItem as TaskModel;

            if (task == null)
                return;

            if (_service.Delete(task))
                RefreshGrid();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            tasksGrid.ItemsSource = _service.Search(tbSearch.Text);
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            if (cbFilter.SelectedItem == null)
                return;

            var status = (TaskProcStatus)cbFilter.SelectedItem;

            tasksGrid.ItemsSource =
                _service.GetTasksByStatus(status);
        }


        private void SaveJson_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = "JSON files (*.json)|*.json";

            if (dialog.ShowDialog() == true)
            {
                _jsonService.Save(
                    dialog.FileName,
                    _service.GetAllTasks());

                MessageBox.Show("Файл сохранён.");
            }
        }

        private void LoadJson_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "JSON files (*.json)|*.json";

            if (dialog.ShowDialog() == true)
            {
                List<TaskModel> tasks =
                    _jsonService.Load(dialog.FileName);

                _service.GetAllTasks().Clear();
                _service.GetAllTasks().AddRange(tasks);

                RefreshGrid();
            }
        }

        private void SaveXml_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = "XML files (*.xml)|*.xml";

            if (dialog.ShowDialog() == true)
            {
                _xmlService.Save(
                    dialog.FileName,
                    _service.GetAllTasks());

                MessageBox.Show("Файл сохранён.");
            }
        }

        private void LoadXml_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "XML files (*.xml)|*.xml";

            if (dialog.ShowDialog() == true)
            {
                List<TaskModel> tasks =
                    _xmlService.Load(dialog.FileName);

                _service.GetAllTasks().Clear();
                _service.GetAllTasks().AddRange(tasks);

                RefreshGrid();
            }
        }
    }
}