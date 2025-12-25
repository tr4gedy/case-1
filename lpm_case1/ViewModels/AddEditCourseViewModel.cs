using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using lpm_case1.Models;
using lpm_case1.Services;
using System;
using System.ComponentModel.DataAnnotations;

namespace lpm_case1.ViewModels
{
    public partial class AddEditCourseViewModel : ObservableValidator
    {
        private readonly ICourseService _courseService;
        private readonly bool _isEditMode;
        private readonly long _courseId;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Название курса обязательно")]
        [MinLength(1, ErrorMessage = "Название курса должно содержать больше одного символа")]
        private string _title = string.Empty;

        [ObservableProperty]
        private string _description = string.Empty;

        [ObservableProperty]
        private CourseCategory _category = CourseCategory.Programming;

        [ObservableProperty]
        private DifficultyLevel _difficultyLevel = DifficultyLevel.Beginner;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Range(0.1, 1000, ErrorMessage = "Продолжительность должна быть от 0.1 до 1000 часов")]
        private double _totalDuration = 10;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Status))]
        private double _progress;

        [ObservableProperty]
        private DateTime? _startDate;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Status))]
        private DateTime? _endDate;

        [ObservableProperty]
        [CustomValidation(typeof(AddEditCourseViewModel), nameof(ValidateResourceLink))]
        private string? _resourceLink;

        public CourseStatus Status =>
            Progress == 0 ? CourseStatus.NotStarted :
            Progress >= 100 ? CourseStatus.Completed :
            CourseStatus.InProgress;

        public List<CourseCategory> Categories { get; } = Enum.GetValues(typeof(CourseCategory))
            .Cast<CourseCategory>()
            .ToList();

        public List<DifficultyLevel> DifficultyLevels { get; } = Enum.GetValues(typeof(DifficultyLevel))
            .Cast<DifficultyLevel>()
            .ToList();

        public string WindowTitle => _isEditMode ? "Редактировать курс" : "Добавить курс";

        public event EventHandler<bool>? DialogClosed;

        public AddEditCourseViewModel(ICourseService courseService) : this(courseService, null)
        {
        }

        public AddEditCourseViewModel(ICourseService courseService, Course? course = null)
        {
            _courseService = courseService;

            if (course != null)
            {
                _isEditMode = true;
                _courseId = course.Id;
                Title = course.Title;
                Description = course.Description;
                Category = course.Category;
                DifficultyLevel = course.DifficultyLevel;
                TotalDuration = course.TotalDuration;
                Progress = course.Progress;
                StartDate = course.StartDate;
                EndDate = course.EndDate;
                ResourceLink = course.ResourceLink;
            }
        }

        public static ValidationResult ValidateResourceLink(string? link)
        {
            if (string.IsNullOrWhiteSpace(link))
                return ValidationResult.Success!;

            if (Uri.TryCreate(link, UriKind.Absolute, out var uri) &&
                (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            {
                return ValidationResult.Success!;
            }

            return new ValidationResult("Неверный формат URL");
        }

        [RelayCommand]
        private void Save()
        {
            ValidateAllProperties();
            if (HasErrors) return;

            var course = _isEditMode
                ? _courseService.GetCourseById(_courseId)
                : new Course();

            if (course == null) return;

            // Обновляем все свойства
            course.Title = Title;
            course.Description = Description;
            course.Category = Category;
            course.DifficultyLevel = DifficultyLevel;
            course.TotalDuration = TotalDuration;
            course.Progress = Progress;
            course.StartDate = StartDate;
            course.EndDate = Progress >= 100 ? DateTime.Today : null;
            course.ResourceLink = ResourceLink ?? string.Empty;

            if (_isEditMode)
            {
                _courseService.UpdateCourse(course);
            }
            else
            {
                _courseService.AddCourse(course);
            }

            DialogClosed?.Invoke(this, true);
        }

        [RelayCommand]
        private void Cancel()
        {
            DialogClosed?.Invoke(this, false);
        }
    }
}