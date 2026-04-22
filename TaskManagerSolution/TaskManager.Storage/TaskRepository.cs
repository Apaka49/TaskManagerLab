using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Storage;

namespace TaskManager.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagerDbContext _context;

        public TaskRepository(TaskManagerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskStorageModel>> GetAllTasksAsync()
        {
            return await _context.Tasks.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TaskStorageModel>> GetTasksByProjectIdAsync(Guid projectId)
        {
            return await _context.Tasks.AsNoTracking().Where(t => t.ProjectId == projectId).ToListAsync();
        }

        public async Task<TaskStorageModel> GetTaskByIdAsync(Guid id)
        {
            return await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTaskAsync(TaskStorageModel task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TaskStorageModel task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            var task = await GetTaskByIdAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}