using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
        private bool _isEditMode;
        private List<TaskListDto> _allTasks;
        private string _searchText;
        private bool _isSortedAscending = true;

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

        public bool IsEditMode
        {
            get => _isEditMode;
            set { _isEditMode = value; OnPropertyChanged(); }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFiltersAndSort();
            }
        }

        public ICommand BackCommand { get; }
        public ICommand ToggleEditModeCommand { get; }
        public ICommand SaveProjectCommand { get; }
        public ICommand DeleteProjectCommand { get; }
        public ICommand SortCommand { get; }

        public ProjectDetailsViewModel(IProjectService projectService, ITaskService taskService, MainViewModel mainViewModel)
        {
            _projectService = projectService;
            _taskService = taskService;
            _mainViewModel = mainViewModel;
            Tasks = new ObservableCollection<TaskListDto>();
            _allTasks = new List<TaskListDto>();

            BackCommand = new RelayCommand(_ => _mainViewModel.NavigateToProjectsList());
            ToggleEditModeCommand = new RelayCommand(_ => IsEditMode = !IsEditMode);
            SaveProjectCommand = new AsyncRelayCommand(async _ => await SaveProjectAsync());
            DeleteProjectCommand = new AsyncRelayCommand(async _ => await DeleteProjectAsync());
            SortCommand = new RelayCommand(_ =>
            {
                _isSortedAscending = !_isSortedAscending;
                ApplyFiltersAndSort();
            });
        }

        public async Task InitializeAsync(Guid projectId)
        {
            if (IsBusy) return;
            IsBusy = true;

            Project = await _projectService.GetProjectDetailsAsync(projectId);
            var tasks = await _taskService.GetTasksForProjectAsync(projectId);

            _allTasks = tasks.ToList();
            ApplyFiltersAndSort();

            IsBusy = false;
        }

        private void ApplyFiltersAndSort()
        {
            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? _allTasks
                : _allTasks.Where(t => t.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();

            filtered = _isSortedAscending
                ? filtered.OrderBy(t => t.Title).ToList()
                : filtered.OrderByDescending(t => t.Title).ToList();

            Tasks.Clear();
            foreach (var task in filtered)
            {
                Tasks.Add(task);
            }
        }

        private async Task SaveProjectAsync()
        {
            if (Project == null || IsBusy) return;
            IsBusy = true;
            await _projectService.UpdateProjectAsync(Project);
            IsEditMode = false;
            IsBusy = false;
        }

        private async Task DeleteProjectAsync()
        {
            if (Project == null || IsBusy) return;
            IsBusy = true;
            await _projectService.DeleteProjectAsync(Project.Id);
            IsBusy = false;
            _mainViewModel.NavigateToProjectsList();
        }
    }
}