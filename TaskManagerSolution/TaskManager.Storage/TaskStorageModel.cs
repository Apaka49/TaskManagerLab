using System;

namespace TaskManager.Storage
{
    public class TaskStorageModel
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public ProjectStorageModel Project { get; set; }

        protected TaskStorageModel() { }

        public TaskStorageModel(Guid id, Guid projectId, string title, string description, TaskPriority priority, DateTime dueDate, bool isCompleted)
        {
            Id = id;
            ProjectId = projectId;
            Title = title;
            Description = description;
            Priority = priority;
            DueDate = dueDate;
            IsCompleted = isCompleted;
        }

        public TaskStorageModel(Guid projectId, string title, string description, TaskPriority priority, DateTime dueDate, bool isCompleted)
            : this(Guid.NewGuid(), projectId, title, description, priority, dueDate, isCompleted)
        {
        }
    }
}