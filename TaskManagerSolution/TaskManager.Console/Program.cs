using System;
using System.Collections.Generic;
using TaskManager.Services;
using TaskManager.Models;
using TaskManager.Storage;

namespace TaskManager.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            StorageService storageService = new StorageService();
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("=== Task Manager ===");

                List<ProjectStorageModel> storageProjects = storageService.GetAllProjects();
                List<ProjectViewModel> viewProjects = new List<ProjectViewModel>();

                foreach (var sp in storageProjects)
                {
                    List<TaskStorageModel> storageTasks = storageService.GetTasksByProjectId(sp.Id);
                    List<TaskViewModel> viewTasks = new List<TaskViewModel>();

                    foreach (var st in storageTasks)
                    {
                        viewTasks.Add(new TaskViewModel(st));
                    }

                    viewProjects.Add(new ProjectViewModel(sp, viewTasks));
                }

                for (int i = 0; i < viewProjects.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {viewProjects[i].Title} ({viewProjects[i].Type}) - Progress: {viewProjects[i].Progress}%");
                }

                Console.WriteLine("0. Exit");
                Console.Write("Select a project: ");

                string input = Console.ReadLine();

                if (input == "0")
                {
                    isRunning = false;
                    continue;
                }

                if (int.TryParse(input, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= viewProjects.Count)
                {
                    ProjectViewModel selectedProject = viewProjects[selectedIndex - 1];
                    ShowProjectDetails(selectedProject);
                }
                else
                {
                    Console.WriteLine("Invalid input. Press Enter to try again.");
                    Console.ReadLine();
                }
            }
        }

        static void ShowProjectDetails(ProjectViewModel project)
        {
            bool inProjectMenu = true;

            while (inProjectMenu)
            {
                Console.Clear();
                Console.WriteLine($"=== Project: {project.Title} ===");
                Console.WriteLine($"Description: {project.Description}");
                Console.WriteLine($"Type: {project.Type}");
                Console.WriteLine($"Progress: {project.Progress}%");
                Console.WriteLine("\nTasks:");

                if (project.Tasks.Count == 0)
                {
                    Console.WriteLine("No tasks found.");
                }
                else
                {
                    for (int i = 0; i < project.Tasks.Count; i++)
                    {
                        var task = project.Tasks[i];
                        string status = task.IsCompleted ? "[Done]" : "[ ]";
                        string overdue = task.IsOverdue ? " (OVERDUE!)" : "";
                        Console.WriteLine($"{i + 1}. {status} {task.Title} (Priority: {task.Priority}){overdue}");
                    }
                }

                Console.WriteLine("\n0. Back to Projects");
                Console.Write("Select an option: ");

                string input = Console.ReadLine();

                if (input == "0")
                {
                    inProjectMenu = false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Press Enter to try again.");
                    Console.ReadLine();
                }
            }
        }
    }
}