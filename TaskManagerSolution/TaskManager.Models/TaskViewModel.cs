using System;
using TaskManager.Storage;

namespace TaskManager.Models
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public bool IsOverdue
        {
            get
            {
                if (IsCompleted)
                {
                    return false;
                }

                return DueDate < DateTime.Now;
            }
        }

        public TaskViewModel(TaskStorageModel storageModel)
        {
            Id = storageModel.Id;
            ProjectId = storageModel.ProjectId;
            Title = storageModel.Title;
            Description = storageModel.Description;
            Priority = storageModel.Priority;
            DueDate = storageModel.DueDate;
            IsCompleted = storageModel.IsCompleted;
        }
    }
}