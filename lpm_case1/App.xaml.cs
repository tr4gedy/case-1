using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using lpm_case1.Data;
using lpm_case1.Services;
using lpm_case1.ViewModels;
using lpm_case1.Views;

namespace lpm_case1
{
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();

     
            serviceCollection.AddScoped<AppDbContext>();
            serviceCollection.AddScoped<ICourseService, CourseService>();
            serviceCollection.AddScoped<IOpenDialogService, OpenDialogService>();

    
            serviceCollection.AddTransient<MainViewModel>();
            serviceCollection.AddTransient<AddEditCourseViewModel>();

      
            serviceCollection.AddTransient<MainWindow>();
            serviceCollection.AddTransient<AddEditCourseWindow>();

            _serviceProvider = serviceCollection.BuildServiceProvider();

  
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureCreated();

            DbInitializer.Initialize(dbContext);

          
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = _serviceProvider.GetRequiredService<MainViewModel>();
            mainWindow.Show();

            base.OnStartup(e);
        }

      
    }
}