using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using lpm_case1.Services;

namespace lpm_case1.Models
{
    public class Course : INotifyPropertyChanged
    {
        private double _progress;
       

        public Course()
        {
            
        }

        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CourseCategory Category { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public double TotalDuration { get; set; }

        public double Progress
        {
            get => _progress;
            set
            {
                _progress = Math.Clamp(value, 0, 100);
                OnPropertyChanged();
                Status = CalculateStatus(_progress);
            }
        }

        public CourseStatus Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ResourceLink { get; set; } = string.Empty;
        public ICollection<ProgressHistory> ProgressHistory { get; set; } = new List<ProgressHistory>();

        private CourseStatus CalculateStatus(double progress)
        {
            return progress switch
            {
                0 => CourseStatus.NotStarted,
                100 => CourseStatus.Completed,
                _ => CourseStatus.InProgress
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
