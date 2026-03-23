using System.Collections.Generic;
using TaskManager.Storage;

namespace TaskManager.Repositories
{
    public interface IProjectRepository
    {
        IEnumerable<ProjectStorageModel> GetAllProjects();
    }
}