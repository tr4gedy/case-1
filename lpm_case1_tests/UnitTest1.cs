
using Moq;
using lpm_case1.Data;
using lpm_case1.Models;
using lpm_case1.Services;
using lpm_case1.ViewModels;
using NUnit.Framework;
using System.Security.RightsManagement;
using System.Windows;
using NUnit.Framework.Legacy;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Collections.Generic;
namespace lpm_case1.Tests
{
    public class Tests
    {
        
        
        [SetUp]
        public void Setup()
        {
          
        }

        [Test]
        public void GetAllCourses_WithValidData()
        {
            // Arrange
            var mockDbContext = new Mock<AppDbContext>();
            var mockDbSet = new Mock<DbSet<Course>>();

            var courses = new List<Course>
            {
                new() { Id = 1, Title = "c++" },
                new() { Id = 2, Title = "c#" },
                new() { Id = 3, Title = "python" },
            }.AsQueryable();

            mockDbSet.As<IQueryable<Course>>().Setup(m => m.Provider).Returns(courses.Provider);
            mockDbSet.As<IQueryable<Course>>().Setup(m => m.Expression).Returns(courses.Expression);
            mockDbSet.As<IQueryable<Course>>().Setup(m => m.ElementType).Returns(courses.ElementType);
            mockDbSet.As<IQueryable<Course>>().Setup(m => m.GetEnumerator()).Returns(courses.GetEnumerator());

            mockDbContext.Setup(x => x.Courses).Returns(mockDbSet.Object);

            var courseService = new CourseService(mockDbContext.Object);

            // Act
            var allCourses = courseService.GetAllCourses();

            // Assert
            ClassicAssert.AreEqual(3, allCourses.Count());
        }
        [Test]
        public void GetCourseByStatus_WithValidData()
        {
            // Arrange
            var mockDbContext = new Mock<AppDbContext>();
            var mockDbSet = new Mock<DbSet<Course>>();

            var courses = new List<Course>
            {
                new() { Id = 1, Title = "c++", Status = CourseStatus.InProgress },
                new() { Id = 2, Title = "c#", Status = CourseStatus.InProgress },
                new() { Id = 3, Title = "python" , Status = CourseStatus.Completed},
            }.AsQueryable();

            mockDbSet.As<IQueryable<Course>>().Setup(m => m.Provider).Returns(courses.Provider);
            mockDbSet.As<IQueryable<Course>>().Setup(m => m.Expression).Returns(courses.Expression);
            mockDbSet.As<IQueryable<Course>>().Setup(m => m.ElementType).Returns(courses.ElementType);
            mockDbSet.As<IQueryable<Course>>().Setup(m => m.GetEnumerator()).Returns(courses.GetEnumerator());

            mockDbContext.Setup(x => x.Courses).Returns(mockDbSet.Object);

            var courseService = new CourseService(mockDbContext.Object);

            // Act
            var coursesByStatus = courseService.GetCoursesByStatus(CourseStatus.InProgress);

            // Assert
            ClassicAssert.AreEqual(2, coursesByStatus.Count());
        }
        [Test]
        public void AddCourse_WithValidData()
        {
            // Arrange
            var mockDbContext = new Mock<AppDbContext>();
            var mockDbSet = new Mock<DbSet<Course>>();
            var newCourse = new List<Course>(); 
            var courses = new List<Course>
            {
                new() { Id = 1, Title = "c++", Status = CourseStatus.InProgress },
                new() { Id = 2, Title = "c#", Status = CourseStatus.InProgress },
                new() { Id = 3, Title = "python" , Status = CourseStatus.Completed},
            }.AsQueryable();

            mockDbSet.As<IQueryable<Course>>().Setup(m => m.Provider).Returns(courses.Provider);
            mockDbSet.As<IQueryable<Course>>().Setup(m => m.Expression).Returns(courses.Expression);
            mockDbSet.As<IQueryable<Course>>().Setup(m => m.ElementType).Returns(courses.ElementType);
            mockDbSet.As<IQueryable<Course>>().Setup(m => m.GetEnumerator()).Returns(courses.GetEnumerator());
            mockDbSet.Setup(d => d.Add(It.IsAny<Course>())).Callback<Course>((s) => newCourse.Add(s));
            mockDbContext.Setup(x => x.Courses).Returns(mockDbSet.Object);

            var courseService = new CourseService(mockDbContext.Object);

            // Act
            var addCource = courseService.AddCourseTest(new() { Id = 0, Title = "asm" });

            // Assert
            ClassicAssert.AreEqual(true, addCource);
        }
    }
}