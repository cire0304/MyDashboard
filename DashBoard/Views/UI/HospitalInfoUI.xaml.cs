using System.Windows.Controls;
using DashBoard.ViewModels;

namespace DashBoard.Views.UI
{
    public partial class HospitalInfoUI : UserControl
    {
        public HospitalInfoUI()
        {
            this.DataContext = App.Current.Services.GetService(typeof(HospitalInfoViewModel));
            InitializeComponent();
        }
    }
}
