using System.ComponentModel;
using DashBoard.Stores;
using DashBoard.ViewModels;

namespace DashBoard.Services
{
    class NavigationService : INavigationService
    {
        private readonly MainNavigationStore _mainNavigationStore;
        private INotifyPropertyChanged? _currentViewModel
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
                    _currentViewModel = (INotifyPropertyChanged?)App.Current.Services.GetService(typeof(HomeViewModel));
                    break;
                case NaviType.CustomerView:
                    _currentViewModel = (INotifyPropertyChanged?)App.Current.Services.GetService(typeof(CustomerViewModel));
                    break;
            }            
        }
    }
}
