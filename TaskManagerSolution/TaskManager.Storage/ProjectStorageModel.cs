using System;

namespace TaskManager.Storage
{
    public class ProjectStorageModel
    {
        public Guid Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ProjectType Type { get; set; }

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