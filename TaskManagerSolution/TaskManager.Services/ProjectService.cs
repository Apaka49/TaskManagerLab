using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Repositories;
using TaskManager.Services.DTOs;

namespace TaskManager.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public IEnumerable<ProjectListDto> GetAllProjects()
        {
            return _projectRepository.GetAllProjects().Select(p => new ProjectListDto
            {
                Id = p.Id,
                Title = p.Title,
                Type = p.Type
            });
        }

        public ProjectDetailDto GetProjectDetails(Guid projectId)
        {
            var project = _projectRepository.GetAllProjects().FirstOrDefault(p => p.Id == projectId);
            if (project == null) return null;

            return new ProjectDetailDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                Type = project.Type
            };
        }
    }
}