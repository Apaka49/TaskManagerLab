using System;
using TaskManager.Repositories;
using TaskManager.Storage;

namespace TaskManager.Services.DTOs
{
    public class ProjectDetailDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ProjectType Type { get; set; }
    }
}