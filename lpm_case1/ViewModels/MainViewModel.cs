using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using lpm_case1.Models;
using lpm_case1.Services;
using lpm_case1.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace lpm_case1.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ICourseService _courseService;
        private readonly IOpenDialogService _dialogService;

        [ObservableProperty]
        private ObservableCollection<Course> _courses = new();

        [ObservableProperty]
        private Course? _selectedCourse;

        [ObservableProperty]
        private string _searchQuery = string.Empty;

        [ObservableProperty]
        private CourseCategory? _selectedCategoryFilter;

        public List<CourseCategory> Categories { get; } = Enum.GetValues(typeof(CourseCategory))
            .Cast<CourseCategory>()
            .ToList();

        public MainViewModel(ICourseService courseService, IOpenDialogService dialogService)
        {
            _courseService = courseService;
            _dialogService = dialogService;
            LoadCourses();
        }

        private void LoadCourses()
        {
            var courses = _courseService.GetAllCourses();
            Courses = new ObservableCollection<Course>(courses);
        }

        [RelayCommand]
        private void FilterCourses()
        {
            var filtered = _courseService.GetAllCourses().AsQueryable();

            if (!string.IsNullOrEmpty(SearchQuery))
                filtered = filtered.Where(c => c.Title.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));

            if (SelectedCategoryFilter.HasValue)
                filtered = filtered.Where(c => c.Category == SelectedCategoryFilter.Value);

            Courses = new ObservableCollection<Course>(filtered.ToList());
        }

        [RelayCommand]
        private void OpenAddCourseDialog()
        {
            var vm = new AddEditCourseViewModel(_courseService);
            vm.DialogClosed += (sender, dialogResult) =>
            {
                if (dialogResult)
                {
                    LoadCourses();
                }
            };

            var dialog = new AddEditCourseWindow { DataContext = vm };
            _dialogService.ShowDialog(dialog);
        }

        [RelayCommand]
        private void OpenEditCourseDialog()
        {
            if (SelectedCourse == null) return;

            var vm = new AddEditCourseViewModel(_courseService, SelectedCourse);
            vm.DialogClosed += (sender, dialogResult) =>
            {
                if (dialogResult)
                {
                    LoadCourses();
                }
            };

            var dialog = new AddEditCourseWindow { DataContext = vm };
            _dialogService.ShowDialog(dialog);
        }

        [RelayCommand]
        private void DeleteCourse()
        {
            if (SelectedCourse == null) return;

            var result = MessageBox.Show(
                $"Вы уверены, что хотите удалить курс '{SelectedCourse.Title}'?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _courseService.DeleteCourse(SelectedCourse.Id);
                LoadCourses();
            }
        }

        [RelayCommand]
        private void QuickSetProgress(object parameter)
        {
            if (SelectedCourse == null) return;

            
            if (int.TryParse(parameter?.ToString(), out int value))
            {
                SelectedCourse.Progress = value;
                _courseService.UpdateCourse(SelectedCourse);
            }
        }

        [RelayCommand]
        private void OpenMaterials()
        {
            if (string.IsNullOrWhiteSpace(SelectedCourse?.ResourceLink))
            {
                MessageBox.Show("Ссылка на материалы не указана", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                if (Uri.TryCreate(SelectedCourse.ResourceLink, UriKind.Absolute, out var uri))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = uri.ToString(),
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("Неверный формат URL", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии материалов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}