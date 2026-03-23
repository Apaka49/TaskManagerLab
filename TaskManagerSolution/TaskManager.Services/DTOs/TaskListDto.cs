using System;
using TaskManager.Repositories;
using TaskManager.Storage;

namespace TaskManager.Services.DTOs
{
    public class TaskListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public TaskPriority Priority { get; set; }
        public bool IsCompleted { get; set; }
    }
}