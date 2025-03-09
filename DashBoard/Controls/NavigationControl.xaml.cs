using System.Windows.Controls;
using DashBoard.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DashBoard.Controls
{
    public partial class NavigationControl : UserControl
    {
        public NavigationControl()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetRequiredService<NavigationViewModel>();
        }
    }
}
