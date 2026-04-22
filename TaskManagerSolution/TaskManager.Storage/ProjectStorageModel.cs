using System;
using System.Collections.Generic;

namespace TaskManager.Storage
{
    public class ProjectStorageModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ProjectType Type { get; set; }

        public List<TaskStorageModel> Tasks { get; set; } = new List<TaskStorageModel>();

        protected ProjectStorageModel() { }

        public ProjectStorageModel(Guid id, string title, string description, ProjectType type)
        {
            Id = id;
            Title = title;
            Description = description;
            Type = type;
        }

        public ProjectStorageModel(string title, string description, ProjectType type)
            : this(Guid.NewGuid(), title, description, type)
        {
        }
    }
}