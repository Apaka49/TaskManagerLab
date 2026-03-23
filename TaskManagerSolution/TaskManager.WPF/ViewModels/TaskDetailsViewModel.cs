using System;
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

        public TaskDetailDto Task
        {
            get => _task;
            set { _task = value; OnPropertyChanged(); }
        }

        public ICommand BackCommand { get; }

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
        }

        public void Initialize(Guid taskId)
        {
            Task = _taskService.GetTaskDetails(taskId);
        }
    }
}