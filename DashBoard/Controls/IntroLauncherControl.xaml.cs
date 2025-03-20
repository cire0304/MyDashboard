using System.Windows.Controls;
using DashBoard.ViewModels;

namespace DashBoard.Controls
{
    /// <summary>
    /// IntroLauncherContrl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class IntroLauncherControl : UserControl
    {
        public IntroLauncherControl()
        {
            InitializeComponent();

            this.DataContext = App.Current.Services.GetService(typeof(IntroLauncherViewModel));
        }
    }
}
