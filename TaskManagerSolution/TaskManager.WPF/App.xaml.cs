using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TaskManager.Repositories;
using TaskManager.Services;
using TaskManager.Storage;
using TaskManager.WPF.ViewModels;

namespace TaskManager.WPF
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            await SeedDatabaseAsync(ServiceProvider);

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            var mainViewModel = ServiceProvider.GetRequiredService<MainViewModel>();

            mainViewModel.NavigateToProjectsList();

            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TaskManagerDbContext>();

            services.AddSingleton<IProjectRepository, ProjectRepository>();
            services.AddSingleton<ITaskRepository, TaskRepository>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<ITaskService, TaskService>();

            services.AddSingleton<MainViewModel>();
            services.AddTransient<MainWindow>();
            services.AddTransient<ProjectsViewModel>();
            services.AddTransient<ProjectDetailsViewModel>();
            services.AddTransient<TaskDetailsViewModel>();
        }

        private async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TaskManagerDbContext>();

            await context.Database.EnsureCreatedAsync();

            if (!context.Projects.Any())
            {
                var project1 = new ProjectStorageModel("First Lab Project", "Initial project for testing", ProjectType.Personal);
                var project2 = new ProjectStorageModel("Work Project", "Important tasks", ProjectType.Work);

                await context.Projects.AddRangeAsync(project1, project2);
                await context.SaveChangesAsync();

                var task1 = new TaskStorageModel(project1.Id, "Setup Repository", "Create GitHub repo", TaskPriority.High, DateTime.Now.AddDays(1), false);
                var task2 = new TaskStorageModel(project1.Id, "Write Code", "Implement MVVM", TaskPriority.Medium, DateTime.Now.AddDays(2), false);
                var task3 = new TaskStorageModel(project2.Id, "Prepare Report", "Write Word document", TaskPriority.Critical, DateTime.Now.AddDays(3), false);

                await context.Tasks.AddRangeAsync(task1, task2, task3);
                await context.SaveChangesAsync();
            }
        }
    }
}