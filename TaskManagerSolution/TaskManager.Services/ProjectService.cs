using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Repositories;
using TaskManager.Services.DTOs;
using TaskManager.Storage;

namespace TaskManager.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectListDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();
            return projects.Select(p => new ProjectListDto
            {
                Id = p.Id,
                Title = p.Title,
                Type = p.Type
            });
        }

        public async Task<ProjectDetailDto> GetProjectDetailsAsync(Guid projectId)
        {
            var project = await _projectRepository.GetProjectByIdAsync(projectId);
            if (project == null) return null;

            return new ProjectDetailDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Type = project.Type
            };
        }

        public async Task AddProjectAsync(ProjectDetailDto projectDto)
        {
            var project = new ProjectStorageModel(projectDto.Title, projectDto.Description, projectDto.Type);
            await _projectRepository.AddProjectAsync(project);
        }

        public async Task UpdateProjectAsync(ProjectDetailDto projectDto)
        {
            var project = new ProjectStorageModel(projectDto.Id, projectDto.Title, projectDto.Description, projectDto.Type);
            await _projectRepository.UpdateProjectAsync(project);
        }

        public async Task DeleteProjectAsync(Guid projectId)
        {
            await _projectRepository.DeleteProjectAsync(projectId);
        }
    }
}