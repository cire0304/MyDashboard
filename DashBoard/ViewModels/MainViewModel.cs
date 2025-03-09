using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FontAwesome.Sharp;

namespace DashBoard.ViewModels
{
    [ObservableObject]
    public partial class MainViewModel
    {
        [ObservableProperty]
        private ViewModelBase? _currentChildViewModel;
        [ObservableProperty]
        private string? _caption;
        [ObservableProperty]
        private IconChar? _icon;


        public MainViewModel()
        {
            LoadDate();
        }



        [RelayCommand]
        public void ShowHomeViewCommand()
        {
            CurrentChildViewModel = new HomeViewModel();
            Caption = "Home";
            Icon = IconChar.Home;
        }

        [RelayCommand]
        public void ShowOtherViewCommand()
        {
            CurrentChildViewModel = new CustomerViewModel();
            Caption = "Customer";
            Icon = IconChar.UserGroup;
        }

        private void LoadDate()
        {
         
            
        }
    }
}
