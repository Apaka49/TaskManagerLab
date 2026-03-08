using System;
using System.Collections.Generic;
using TaskManager.Storage;

namespace TaskManager.Services
{
    public interface IStorageService
    {
        List<ProjectStorageModel> GetAllProjects();
        List<TaskStorageModel> GetAllTasks();
        List<TaskStorageModel> GetTasksByProjectId(Guid projectId);
    }
}