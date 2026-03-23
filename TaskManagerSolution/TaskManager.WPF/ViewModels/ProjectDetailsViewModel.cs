using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskManager.Services;
using TaskManager.Services.DTOs;

namespace TaskManager.WPF.ViewModels
{
    public class ProjectDetailsViewModel : ViewModelBase
    {
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;
        private readonly MainViewModel _mainViewModel;
        private ProjectDetailDto _project;
        private TaskListDto _selectedTask;

        public ProjectDetailDto Project
        {
            get => _project;
            set { _project = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TaskListDto> Tasks { get; set; }

        public TaskListDto SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged();
                if (_selectedTask != null)
                {
                    _mainViewModel.NavigateToTaskDetails(_selectedTask.Id);
                }
            }
        }

        public ICommand BackCommand { get; }

        public ProjectDetailsViewModel(IProjectService projectService, ITaskService taskService, MainViewModel mainViewModel)
        {
            _projectService = projectService;
            _taskService = taskService;
            _mainViewModel = mainViewModel;

            BackCommand = new RelayCommand(_ => _mainViewModel.NavigateToProjectsList());
        }

        public void Initialize(Guid projectId)
        {
            Project = _projectService.GetProjectDetails(projectId);
            Tasks = new ObservableCollection<TaskListDto>(_taskService.GetTasksForProject(projectId));
            OnPropertyChanged(nameof(Tasks));
        }
    }
}