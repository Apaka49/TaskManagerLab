using System;
using TaskManager.Repositories;
using TaskManager.Storage;

namespace TaskManager.Services.DTOs
{
    public class TaskDetailDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}