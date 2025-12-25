using lpm_case1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lpm_case1.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditCourseWindow.xaml
    /// </summary>
    public partial class AddEditCourseWindow : Window
    {
        public AddEditCourseWindow()
        {
            InitializeComponent();
            Loaded += AddEditCourseWindow_Loaded;
        }

        private void AddEditCourseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddEditCourseViewModel viewModel)
            {
                viewModel.DialogClosed += (s, dialogResult) =>
                {
                    DialogResult = dialogResult;
                    Close();
                };
            }
        }
    }
}
