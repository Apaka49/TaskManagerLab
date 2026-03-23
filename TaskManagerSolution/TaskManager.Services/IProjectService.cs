using System;
using System.Collections.Generic;
using TaskManager.Services.DTOs;

namespace TaskManager.Services
{
    public interface IProjectService
    {
        IEnumerable<ProjectListDto> GetAllProjects();
        ProjectDetailDto GetProjectDetails(Guid projectId);
    }
}