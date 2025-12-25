using lpm_case1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpm_case1.Services
{
    public interface ICourseService
    {
        List<Course> GetAllCourses();
        Course? GetCourseById(long id);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(long id);
        IEnumerable<Course> GetStaleCourses(int daysThreshold = 30);
        double CalculateRemainingTime(Course course);
    }
}
