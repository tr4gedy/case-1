using System.Windows;
using System.Windows.Controls;

namespace lpm_case1.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupSearchBoxPlaceholder();
        }

        private void SetupSearchBoxPlaceholder()
        {
            var searchBox = this.FindName("SearchBox") as TextBox;
            var placeholderText = this.FindName("PlaceholderText") as TextBlock;

            if (searchBox != null && placeholderText != null)
            {
                searchBox.GotFocus += (s, e) =>
                {
                    placeholderText.Visibility = Visibility.Collapsed;
                };

                searchBox.LostFocus += (s, e) =>
                {
                    placeholderText.Visibility = string.IsNullOrEmpty(searchBox.Text)
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                };

                searchBox.TextChanged += (s, e) =>
                {
                    placeholderText.Visibility = string.IsNullOrEmpty(searchBox.Text)
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                };

                placeholderText.Visibility = string.IsNullOrEmpty(searchBox.Text)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }
    }
}