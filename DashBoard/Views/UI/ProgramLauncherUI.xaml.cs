using System.Windows.Controls;
using DashBoard.ViewModels;

namespace DashBoard.Views.UI
{
    public partial class ProgramLauncherUI : UserControl
    {
        public ProgramLauncherUI()
        {
            this.DataContext = App.Current.Services.GetService(typeof(ProgramLancherViewModel));
            InitializeComponent();
        }
    }
}
