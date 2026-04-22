using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Storage;

namespace TaskManager.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectStorageModel>> GetAllProjectsAsync();
        Task<ProjectStorageModel> GetProjectByIdAsync(Guid id);
        Task AddProjectAsync(ProjectStorageModel project);
        Task UpdateProjectAsync(ProjectStorageModel project);
        Task DeleteProjectAsync(Guid id);
    }
}