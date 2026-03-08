using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Services;
using TaskManager.Storage;

namespace TaskManager.WPF
{
    public partial class ProjectsPage : Page
    {
        private readonly IStorageService _storageService;

        public ProjectsPage(IStorageService storageService)
        {
            InitializeComponent();
            _storageService = storageService;
            LoadData();
        }

        private void LoadData()
        {
            ProjectsList.ItemsSource = _storageService.GetAllProjects();
        }

        private void ProjectsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProjectsList.SelectedItem is ProjectStorageModel selectedProject)
            {
                var detailsPage = App.ServiceProvider.GetRequiredService<ProjectDetailsPage>();
                detailsPage.LoadProject(selectedProject);

                this.NavigationService.Navigate(detailsPage);

                ProjectsList.SelectedItem = null;
            }
        }
    }
}