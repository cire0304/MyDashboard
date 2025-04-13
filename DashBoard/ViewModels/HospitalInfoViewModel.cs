using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using DashBoard.Services;
using DashBoard.Stores;

namespace DashBoard.ViewModels
{
    public partial class HospitalInfoViewModel : ObservableObject, IViewModelBase
    {
        private readonly HospitalInfomationService _hospitalInfomationService;
        
        [ObservableProperty]
        private string _id = "";
        [ObservableProperty]
        private string _password = "";
        [ObservableProperty]
        private string _hospitalInfo;

        private MainStore _mainStore;

        public HospitalInfoViewModel(HospitalInfomationService hospitalInfomationService, MainStore mainStore)
        {
            _mainStore = mainStore;
            _hospitalInfomationService = hospitalInfomationService;
            
            _hospitalInfo = _hospitalInfomationService.ReadHospitalInfomation();

            _hospitalInfomationService.HospitalInfoChanged += content =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    _hospitalInfo = content;
                });
            };
        }

        partial void OnIdChanged(string value)
        {
            _mainStore.Id = value;
        }

        partial void OnPasswordChanged(string value)
        {
            _mainStore.Password = value;
        }

        partial void OnHospitalInfoChanged(string value)
        {
            _hospitalInfomationService.UpdateHospitalCode(value);
        }
    }
}
