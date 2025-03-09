using System.Collections.Specialized;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DashBoard.Stores
{
    [ObservableObject]
    public partial class MainNavigationStore
    {
		[ObservableProperty]
        private INotifyPropertyChanged? _currentViewModel;

        partial void OnCurrentViewModelChanged(INotifyPropertyChanged? oldValue, INotifyPropertyChanged? newValue)
        {
            CurrentViewModelChanged?.Invoke();
            _currentViewModel = null;
        }

        public Action? CurrentViewModelChanged { get; set; }
	}
}
