using CommunityToolkit.Mvvm.Input;
using DashBoard.Services;

namespace DashBoard.ViewModels
{
    public partial class NavigationViewModel
    {
        private readonly INavigationService _navigationService;

        public NavigationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        public void ToHomeCommand()
        {
            _navigationService.Navigate(NaviType.HomeView);
        }

        [RelayCommand]
        public void ToCustomerCommand()
        {
            _navigationService.Navigate(NaviType.CustomerView);
        }
    }
}
