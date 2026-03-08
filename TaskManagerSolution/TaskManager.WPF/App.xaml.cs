using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Services;

namespace TaskManager.WPF
{
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IStorageService, StorageService>();

            serviceCollection.AddTransient<MainWindow>();
            serviceCollection.AddTransient<ProjectsPage>();
            serviceCollection.AddTransient<ProjectDetailsPage>();
            serviceCollection.AddTransient<TaskDetailsPage>();
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            var startPage = ServiceProvider.GetRequiredService<ProjectsPage>();
            mainWindow.NavigateToPage(startPage);
        }
    }
}