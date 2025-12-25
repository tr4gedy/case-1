using lpm_case1.Data;
using lpm_case1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpm_case1.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;

        public CourseService(AppDbContext context) => _context = context;

        public List<Course> GetAllCourses()
        {
            return _context.Courses.ToList();
        }

        public Course? GetCourseById(long id)
        {
            
            return _context.Courses.Find(id);
        }

        public void AddCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges(); 
        }
        public bool AddCourseTest(Course course)
        {
            if (course != null)
            {
                _context.Courses.Add(course);
                _context.SaveChanges();

            return true;
            }
            else
            {
                return false;
            }
        }
        public void UpdateCourse(Course course)
        {
            var existingCourse = _context.Courses.Find(course.Id);
            if (existingCourse == null) return;

            existingCourse.Title = course.Title;
            existingCourse.Description = course.Description;
            existingCourse.Category = course.Category;
            existingCourse.DifficultyLevel = course.DifficultyLevel;
            existingCourse.TotalDuration = course.TotalDuration;
            existingCourse.Progress = course.Progress;
            existingCourse.StartDate = course.StartDate;
            existingCourse.EndDate = course.EndDate;
            existingCourse.ResourceLink = course.ResourceLink;

            _context.SaveChanges();
        }

        public void DeleteCourse(long id)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges(); 
            }
        }

        public IEnumerable<Course> GetStaleCourses(int daysThreshold = 30)
        {
            var staleDate = DateTime.Today.AddDays(-daysThreshold);
            return _context.Courses
                .Where(c => c.Status == CourseStatus.InProgress &&
                            (!c.ProgressHistory.Any() ||
                             c.ProgressHistory.Max(ph => ph.Timestamp) < staleDate))
                .ToList();
        }

        public double CalculateRemainingTime(Course course)
        {
            if (course.Progress >= 100) return 0;
            return course.TotalDuration * (100 - course.Progress) / 100;
        }
        public IEnumerable<Course> GetCoursesByStatus(CourseStatus status)
        {
            return _context.Courses
                .Where(c => c.Status == status)
                .ToList();
        }

        public void UpdateProgress(long courseId, double newProgress)
        {
            var course = _context.Courses.Find(courseId);
            if (course == null) return;

            course.Progress = newProgress;
            _context.SaveChanges();
        }
    }
}