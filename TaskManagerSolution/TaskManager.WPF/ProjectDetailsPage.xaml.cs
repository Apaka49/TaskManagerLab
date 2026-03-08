using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using TaskManager.Services;
using TaskManager.Storage;

namespace TaskManager.WPF
{
    public partial class ProjectDetailsPage : Page
    {
        private readonly IStorageService _storageService;
        private ProjectStorageModel _currentProject;

        public ProjectDetailsPage(IStorageService storageService)
        {
            InitializeComponent();
            _storageService = storageService;
        }

        public void LoadProject(ProjectStorageModel project)
        {
            _currentProject = project;

            ProjectTitleText.Text = _currentProject.Title;
            ProjectDescriptionText.Text = _currentProject.Description;
            ProjectTypeText.Text = _currentProject.Type.ToString();

            TasksList.ItemsSource = _storageService.GetTasksByProjectId(_currentProject.Id);
        }

        private void TasksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TasksList.SelectedItem is TaskStorageModel selectedTask)
            {
                var taskPage = App.ServiceProvider.GetRequiredService<TaskDetailsPage>();
                taskPage.LoadTask(selectedTask);
                this.NavigationService.Navigate(taskPage);
                TasksList.SelectedItem = null;
            }
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