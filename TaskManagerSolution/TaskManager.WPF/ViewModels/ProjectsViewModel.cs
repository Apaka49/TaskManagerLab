using System.Collections.ObjectModel;
using TaskManager.Services;
using TaskManager.Services.DTOs;

namespace TaskManager.WPF.ViewModels
{
    public class ProjectsViewModel : ViewModelBase
    {
        private readonly IProjectService _projectService;
        private readonly MainViewModel _mainViewModel;
        private ProjectListDto _selectedProject;

        public ObservableCollection<ProjectListDto> Projects { get; set; }

        public ProjectListDto SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;
                OnPropertyChanged();
                if (_selectedProject != null)
                {
                    _mainViewModel.NavigateToProjectDetails(_selectedProject.Id);
                }
            }
        }

        public ProjectsViewModel(IProjectService projectService, MainViewModel mainViewModel)
        {
            _projectService = projectService;
            _mainViewModel = mainViewModel;
            Projects = new ObservableCollection<ProjectListDto>(_projectService.GetAllProjects());
        }
    }
}