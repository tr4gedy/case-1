using System.Windows;
using lpm_case1.Services;

namespace lpm_case1.Services
{
    public class OpenDialogService : IOpenDialogService
    {
        public bool? ShowDialog(Window window)
        {
            return window.ShowDialog();
        }
    }
}