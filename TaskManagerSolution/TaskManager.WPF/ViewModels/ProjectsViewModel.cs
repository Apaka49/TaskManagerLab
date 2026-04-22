using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManager.Services;
using TaskManager.Services.DTOs;
using TaskManager.Storage;

namespace TaskManager.WPF.ViewModels
{
    public class ProjectsViewModel : ViewModelBase
    {
        private readonly IProjectService _projectService;
        private readonly MainViewModel _mainViewModel;
        private ProjectListDto _selectedProject;
        private ObservableCollection<ProjectListDto> _projects;
        private List<ProjectListDto> _allProjects;
        private string _searchText;
        private bool _isSortedAscending = true;

        public ObservableCollection<ProjectListDto> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                OnPropertyChanged();
            }
        }

        public ProjectListDto SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;
                OnPropertyChanged();
                if (_selectedProject != null)
                {
                    _mainViewModel.NavigateToProjectDetails(_selectedProject.Id);
                }
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFiltersAndSort();
            }
        }

        public ICommand SortCommand { get; }
        public ICommand AddProjectCommand { get; }

        public ProjectsViewModel(IProjectService projectService, MainViewModel mainViewModel)
        {
            _projectService = projectService;
            _mainViewModel = mainViewModel;
            Projects = new ObservableCollection<ProjectListDto>();
            _allProjects = new List<ProjectListDto>();

            SortCommand = new RelayCommand(_ =>
            {
                _isSortedAscending = !_isSortedAscending;
                ApplyFiltersAndSort();
            });

            AddProjectCommand = new AsyncRelayCommand(async _ => await AddNewProjectAsync());
        }

        public async Task LoadProjectsAsync()
        {
            if (IsBusy) return;

            IsBusy = true;

            var projects = await _projectService.GetAllProjectsAsync();
            _allProjects = projects.ToList();
            ApplyFiltersAndSort();

            IsBusy = false;
        }

        private void ApplyFiltersAndSort()
        {
            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? _allProjects
                : _allProjects.Where(p => p.Title.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase)).ToList();

            filtered = _isSortedAscending
                ? filtered.OrderBy(p => p.Title).ToList()
                : filtered.OrderByDescending(p => p.Title).ToList();

            Projects.Clear();
            foreach (var project in filtered)
            {
                Projects.Add(project);
            }
        }

        private async Task AddNewProjectAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            var newProject = new ProjectDetailDto
            {
                Title = "New Project",
                Description = "Description",
                Type = ProjectType.Personal
            };

            await _projectService.AddProjectAsync(newProject);

            var projects = await _projectService.GetAllProjectsAsync();
            _allProjects = projects.ToList();
            ApplyFiltersAndSort();

            IsBusy = false;
        }
    }
}