using System.Windows;
using System.Windows.Controls;
using TaskManager.Storage;

namespace TaskManager.WPF
{
    public partial class TaskDetailsPage : Page
    {
        private TaskStorageModel _currentTask;

        public TaskDetailsPage()
        {
            InitializeComponent();
        }

        public void LoadTask(TaskStorageModel task)
        {
            _currentTask = task;

            TaskTitleText.Text = _currentTask.Title;
            TaskDescriptionText.Text = _currentTask.Description;
            TaskPriorityText.Text = $"Priority: {_currentTask.Priority}";
            TaskDeadlineText.Text = $"Deadline: {_currentTask.DueDate:d}";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService != null && this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }
    }
}