using System;
using System.Collections.Generic;
using TaskManager.Storage;

namespace TaskManager.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public IEnumerable<TaskStorageModel> GetAllTasks()
        {
            return FakeStorage.Tasks;
        }

        public IEnumerable<TaskStorageModel> GetTasksByProjectId(Guid projectId)
        {
            var result = new List<TaskStorageModel>();
            foreach (var task in FakeStorage.Tasks)
            {
                if (task.ProjectId == projectId)
                {
                    result.Add(task);
                }
            }
            return result;
        }
    }
}