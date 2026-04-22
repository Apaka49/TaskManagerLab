using System;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Page _currentPage;
        private readonly IServiceProvider _serviceProvider;

        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateToProjectsList()
        {
            var projectsPage = new ProjectsPage();
            projectsPage.DataContext = _serviceProvider.GetRequiredService<ProjectsViewModel>();
            CurrentPage = projectsPage;
        }

        public void NavigateToProjectDetails(Guid projectId)
        {
            var detailsPage = new ProjectDetailsPage();
            var viewModel = _serviceProvider.GetRequiredService<ProjectDetailsViewModel>();
            _ = viewModel.InitializeAsync(projectId);
            detailsPage.DataContext = viewModel;
            CurrentPage = detailsPage;
        }

        public void NavigateToTaskDetails(Guid taskId)
        {
            var taskPage = new TaskDetailsPage();
            var viewModel = _serviceProvider.GetRequiredService<TaskDetailsViewModel>();
            _ = viewModel.InitializeAsync(taskId);
            taskPage.DataContext = viewModel;
            CurrentPage = taskPage;
        }
    }
}