using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Services.DTOs;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskListDto>> GetTasksForProjectAsync(Guid projectId);
        Task<TaskDetailDto> GetTaskDetailsAsync(Guid taskId);
        Task AddTaskAsync(TaskDetailDto taskDto);
        Task UpdateTaskAsync(TaskDetailDto taskDto);
        Task DeleteTaskAsync(Guid taskId);
    }
}