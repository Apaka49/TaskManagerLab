using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Repositories;
using TaskManager.Services.DTOs;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public IEnumerable<TaskListDto> GetTasksForProject(Guid projectId)
        {
            return _taskRepository.GetTasksByProjectId(projectId).Select(t => new TaskListDto
            {
                Id = t.Id,
                Title = t.Title,
                Priority = t.Priority,
                IsCompleted = t.IsCompleted
            });
        }

        public TaskDetailDto GetTaskDetails(Guid taskId)
        {
            var task = _taskRepository.GetAllTasks().FirstOrDefault(t => t.Id == taskId);
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
    }
}