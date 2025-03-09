using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DashBoard.Services;
using DashBoard.Stores;
using FontAwesome.Sharp;

namespace DashBoard.ViewModels
{
    [ObservableObject]
    public partial class MainViewModel
    {
        private readonly MainNavigationStore _mainNigationStore;
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
        public MainViewModel(MainNavigationStore mainNigationStore, INavigationService navigationService)
        {
            _mainNigationStore = mainNigationStore;
            _mainNigationStore.CurrentViewModelChanged += CurrentViewModelChanged;
            navigationService.Navigate(NaviType.HomeView);
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
