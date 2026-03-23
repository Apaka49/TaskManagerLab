using System.Collections.Generic;
using TaskManager.Storage;

namespace TaskManager.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        public IEnumerable<ProjectStorageModel> GetAllProjects()
        {
            return FakeStorage.Projects;
        }
    }
}