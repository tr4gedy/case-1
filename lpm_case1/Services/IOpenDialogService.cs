using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lpm_case1.Services
{
    public interface IOpenDialogService
    {
        bool? ShowDialog(Window window);
    }
}
