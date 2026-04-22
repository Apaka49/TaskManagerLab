using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Services.DTOs;

namespace TaskManager.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectListDto>> GetAllProjectsAsync();
        Task<ProjectDetailDto> GetProjectDetailsAsync(Guid projectId);
        Task AddProjectAsync(ProjectDetailDto projectDto);
        Task UpdateProjectAsync(ProjectDetailDto projectDto);
        Task DeleteProjectAsync(Guid projectId);
    }
}