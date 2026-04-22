using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManager.Services;
using TaskManager.Services.DTOs;

namespace TaskManager.WPF.ViewModels
{
    public class TaskDetailsViewModel : ViewModelBase
    {
        private readonly ITaskService _taskService;
        private readonly MainViewModel _mainViewModel;
        private TaskDetailDto _task;
        private bool _isEditMode;

        public TaskDetailDto Task
        {
            get => _task;
            set { _task = value; OnPropertyChanged(); }
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set { _isEditMode = value; OnPropertyChanged(); }
        }

        public ICommand BackCommand { get; }
        public ICommand ToggleEditModeCommand { get; }
        public ICommand SaveTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }

        public TaskDetailsViewModel(ITaskService taskService, MainViewModel mainViewModel)
        {
            _taskService = taskService;
            _mainViewModel = mainViewModel;

            BackCommand = new RelayCommand(_ =>
            {
                if (Task != null)
                {
                    _mainViewModel.NavigateToProjectDetails(Task.ProjectId);
                }
            });

            ToggleEditModeCommand = new RelayCommand(_ => IsEditMode = !IsEditMode);
            SaveTaskCommand = new AsyncRelayCommand(async _ => await SaveTaskAsync());
            DeleteTaskCommand = new AsyncRelayCommand(async _ => await DeleteTaskAsync());
        }

        public async Task InitializeAsync(Guid taskId)
        {
            if (IsBusy) return;
            IsBusy = true;
            Task = await _taskService.GetTaskDetailsAsync(taskId);
            IsBusy = false;
        }

        private async Task SaveTaskAsync()
        {
            if (Task == null || IsBusy) return;
            IsBusy = true;
            await _taskService.UpdateTaskAsync(Task);
            IsEditMode = false;
            IsBusy = false;
        }

        private async Task DeleteTaskAsync()
        {
            if (Task == null || IsBusy) return;
            IsBusy = true;
            var projectId = Task.ProjectId;
            await _taskService.DeleteTaskAsync(Task.Id);
            IsBusy = false;
            _mainViewModel.NavigateToProjectDetails(projectId);
        }
    }
}