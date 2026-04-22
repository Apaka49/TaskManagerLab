using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Repositories;
using TaskManager.Services.DTOs;
using TaskManager.Storage;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskListDto>> GetTasksForProjectAsync(Guid projectId)
        {
            var tasks = await _taskRepository.GetTasksByProjectIdAsync(projectId);
            return tasks.Select(t => new TaskListDto
            {
                Id = t.Id,
                Title = t.Title,
                Priority = t.Priority,
                IsCompleted = t.IsCompleted
            });
        }

        public async Task<TaskDetailDto> GetTaskDetailsAsync(Guid taskId)
        {
            var task = await _taskRepository.GetTaskByIdAsync(taskId);
            if (task == null) return null;

            return new TaskDetailDto
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                DueDate = task.DueDate,
                IsCompleted = task.IsCompleted
            };
        }

        public async Task AddTaskAsync(TaskDetailDto taskDto)
        {
            var task = new TaskStorageModel(taskDto.ProjectId, taskDto.Title, taskDto.Description, taskDto.Priority, taskDto.DueDate, taskDto.IsCompleted);
            await _taskRepository.AddTaskAsync(task);
        }

        public async Task UpdateTaskAsync(TaskDetailDto taskDto)
        {
            var task = new TaskStorageModel(taskDto.Id, taskDto.ProjectId, taskDto.Title, taskDto.Description, taskDto.Priority, taskDto.DueDate, taskDto.IsCompleted);
            await _taskRepository.UpdateTaskAsync(task);
        }

        public async Task DeleteTaskAsync(Guid taskId)
        {
            await _taskRepository.DeleteTaskAsync(taskId);
        }
    }
}