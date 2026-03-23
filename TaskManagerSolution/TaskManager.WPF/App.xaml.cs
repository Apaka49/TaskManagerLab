using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TaskManager.Repositories;
using TaskManager.Services;
using TaskManager.WPF.ViewModels;

namespace TaskManager.WPF
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            var mainViewModel = ServiceProvider.GetRequiredService<MainViewModel>();

            mainViewModel.NavigateToProjectsList();

            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
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
    }
}