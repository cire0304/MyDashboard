using System.ComponentModel;
using DashBoard.Stores;
using DashBoard.ViewModels;

namespace DashBoard.Services
{
    class NavigationService : INavigationService
    {
        private readonly MainNavigationStore _mainNavigationStore;
        private IViewModelBase? _currentViewModel
        {
            set => _mainNavigationStore.CurrentViewModel = value;
        }

        public NavigationService(MainNavigationStore mainNavigationStore)
        {
            this._mainNavigationStore = mainNavigationStore;
        }

        public void Navigate(NaviType naviType)
        {
            switch(naviType)
            {
                case NaviType.HomeView:
                    _currentViewModel = (IViewModelBase?)App.Current.Services.GetService(typeof(HomeViewModel));
                    break;
                case NaviType.ProgramLauncherView:
                    _currentViewModel = (IViewModelBase?)App.Current.Services.GetService(typeof(IntroLauncherViewModel));
                    break;
                case NaviType.CustomerView:
                    _currentViewModel = (IViewModelBase?)App.Current.Services.GetService(typeof(CustomerViewModel));
                    break;
            }            
        }
    }
}
