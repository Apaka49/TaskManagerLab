using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Storage;

namespace TaskManager.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskStorageModel>> GetAllTasksAsync();
        Task<IEnumerable<TaskStorageModel>> GetTasksByProjectIdAsync(Guid projectId);
        Task<TaskStorageModel> GetTaskByIdAsync(Guid id);
        Task AddTaskAsync(TaskStorageModel task);
        Task UpdateTaskAsync(TaskStorageModel task);
        Task DeleteTaskAsync(Guid id);
    }
}