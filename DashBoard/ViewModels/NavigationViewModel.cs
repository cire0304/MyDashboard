using CommunityToolkit.Mvvm.Input;
using DashBoard.Services;

namespace DashBoard.ViewModels
{
    //[ObservableObject]
    public partial class NavigationViewModel
    {
        private readonly INavigationService _navigationService;

        public NavigationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        public void ToHome()
        {
            _navigationService.Navigate(NaviType.HomeView);
        }

        [RelayCommand]
        public void ToCustomer()
        {
            _navigationService.Navigate(NaviType.CustomerView);
        }

        [RelayCommand]
        public void ToProgramLauncher()
        {
            _navigationService.Navigate(NaviType.ProgramLauncherView);
        }
    }
}
