using System.Windows.Controls;
using DashBoard.ViewModels;

namespace DashBoard.Controls
{
    public partial class IntroLauncherControl : UserControl
    {
        public IntroLauncherControl()
        {
            this.DataContext = App.Current.Services.GetService(typeof(IntroLauncherViewModel));

            InitializeComponent();
        }
    }
}
