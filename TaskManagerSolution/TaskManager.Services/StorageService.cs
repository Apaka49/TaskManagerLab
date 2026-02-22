using System;
using System.Collections.Generic;
using TaskManager.Storage;

namespace TaskManager.Services
{
    public class StorageService
    {
        private static readonly List<ProjectStorageModel> Projects = new List<ProjectStorageModel>();
        private static readonly List<TaskStorageModel> Tasks = new List<TaskStorageModel>();

        static StorageService()
        {
            var project1 = new ProjectStorageModel("Website Redesign", "Overhaul the main corporate website", ProjectType.Work);
            var project2 = new ProjectStorageModel("Learn C#", "Complete all C# university assignments", ProjectType.Educational);
            var project3 = new ProjectStorageModel("Home Renovation", "Kitchen and living room updates", ProjectType.Personal);

            Projects.Add(project1);
            Projects.Add(project2);
            Projects.Add(project3);

            for (int i = 1; i <= 10; i++)
            {
                Tasks.Add(new TaskStorageModel(
                    project1.Id,
                    $"Design Task {i}",
                    $"Complete design phase {i} for the website",
                    TaskPriority.Medium,
                    DateTime.Now.AddDays(i),
                    false
                ));
            }

            Tasks.Add(new TaskStorageModel(
                project2.Id,
                "Read Chapter 1",
                "Read the first chapter of the C# book",
                TaskPriority.High,
                DateTime.Now.AddDays(2),
                true
            ));

            Tasks.Add(new TaskStorageModel(
                project2.Id,
                "Finish Lab 1",
                "Complete the first laboratory work",
                TaskPriority.Critical,
                DateTime.Now.AddDays(5),
                false
            ));
        }

        public List<ProjectStorageModel> GetAllProjects()
        {
            return Projects;
        }

        public List<TaskStorageModel> GetAllTasks()
        {
            return Tasks;
        }

        public List<TaskStorageModel> GetTasksByProjectId(Guid projectId)
        {
            var result = new List<TaskStorageModel>();
            foreach (var task in Tasks)
            {
                if (task.ProjectId == projectId)
                {
                    result.Add(task);
                }
            }
            return result;
        }
    }
}