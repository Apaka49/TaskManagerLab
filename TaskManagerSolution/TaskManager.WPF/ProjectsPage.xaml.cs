using System.Windows;
using System.Windows.Controls;
using TaskManager.WPF.ViewModels;

namespace TaskManager.WPF
{
    public partial class ProjectsPage : Page
    {
        public ProjectsPage()
        {
            InitializeComponent();
            Loaded += ProjectsPage_Loaded;
        }

        private async void ProjectsPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ProjectsViewModel viewModel)
            {
                await viewModel.LoadProjectsAsync();
            }
        }
    }
}