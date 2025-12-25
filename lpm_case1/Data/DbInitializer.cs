using lpm_case1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpm_case1.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // Добавление тестовых данных
            if (context.Courses.Any()) return;

            var courses = new Course[]
            {
            new() { Title = "C# Basics", Category = CourseCategory.Programming, DifficultyLevel = DifficultyLevel.Beginner, TotalDuration = 20, Progress = 30 },
            new() { Title = "UI/UX Design", Category = CourseCategory.Design, DifficultyLevel = DifficultyLevel.Intermediate, TotalDuration = 40, Progress = 75 }
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();
            
        }
    }
}
