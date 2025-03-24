using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DashBoard.Services;
using DashBoard.Stores;
using FontAwesome.Sharp;

namespace DashBoard.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        
        private readonly MainNavigationStore _mainNigationStore;
        [ObservableProperty]
        private NavigationViewModel? _navigationViewModel;
        [ObservableProperty]
        private ViewModelBase? _currentChildViewModel;
        [ObservableProperty]
        private string? _caption;
        [ObservableProperty]
        private IconChar? _icon;

        private void CurrentViewModelChanged()
        {
            CurrentChildViewModel = _mainNigationStore.CurrentViewModel as ViewModelBase;
        }
        public MainViewModel(MainNavigationStore mainNigationStore, INavigationService navigationService, NavigationViewModel navigationViewModel)
        {
            _mainNigationStore = mainNigationStore;
            _navigationViewModel = navigationViewModel;

            _mainNigationStore.CurrentViewModelChanged += CurrentViewModelChanged;

            navigationService.Navigate(NaviType.ProgramLauncherView);
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

    }
}
