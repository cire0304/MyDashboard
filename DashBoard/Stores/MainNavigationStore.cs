using System.Collections.Specialized;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using DashBoard.ViewModels;

namespace DashBoard.Stores
{
    public partial class MainNavigationStore : ObservableObject
    {
		[ObservableProperty]
        private IViewModelBase? _currentViewModel;

        partial void OnCurrentViewModelChanged(IViewModelBase? oldValue, IViewModelBase? newValue)
        {
            CurrentViewModelChanged?.Invoke();
            _currentViewModel = null;
        }

        public Action? CurrentViewModelChanged { get; set; }
	}
}
