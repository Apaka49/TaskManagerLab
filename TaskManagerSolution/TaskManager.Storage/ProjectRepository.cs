using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Storage;

namespace TaskManager.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly TaskManagerDbContext _context;

        public ProjectRepository(TaskManagerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectStorageModel>> GetAllProjectsAsync()
        {
            return await _context.Projects.AsNoTracking().ToListAsync();
        }

        public async Task<ProjectStorageModel> GetProjectByIdAsync(Guid id)
        {
            return await _context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProjectAsync(ProjectStorageModel project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(ProjectStorageModel project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            var project = await GetProjectByIdAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }
    }
}