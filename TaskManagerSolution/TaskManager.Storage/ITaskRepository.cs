using System;
using System.Collections.Generic;
using TaskManager.Storage;

namespace TaskManager.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<TaskStorageModel> GetAllTasks();
        IEnumerable<TaskStorageModel> GetTasksByProjectId(Guid projectId);
    }
}