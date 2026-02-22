using System;
using System.Collections.Generic;
using TaskManager.Storage;

namespace TaskManager.Models
{
    public class ProjectViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ProjectType Type { get; set; }

        public List<TaskViewModel> Tasks { get; set; }

        public double Progress
        {
            get
            {
                if (Tasks == null || Tasks.Count == 0)
                {
                    return 0;
                }

                int completedCount = 0;
                foreach (var task in Tasks)
                {
                    if (task.IsCompleted)
                    {
                        completedCount++;
                    }
                }

                return (double)completedCount / Tasks.Count * 100;
            }
        }

        public ProjectViewModel(ProjectStorageModel storageModel, List<TaskViewModel> tasks)
        {
            Id = storageModel.Id;
            Title = storageModel.Title;
            Description = storageModel.Description;
            Type = storageModel.Type;
            Tasks = tasks;
        }
    }
}