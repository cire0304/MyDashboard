using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using DashBoard.Models;
using DashBoard.Services;
using DashBoard.Stores;

namespace DashBoard.ViewModels
{
    public partial class HospitalInfoViewModel : ObservableObject, IViewModelBase
    {
        private readonly HospitalInfomationService _hospitalInfomationService;

        [ObservableProperty]
        private string _name = "";
        [ObservableProperty]
        private string _id = "";
        [ObservableProperty]
        private string _password = "";

        private HospitalInfo _hospitalInfo;
        private MainStore _mainStore;

        public HospitalInfoViewModel(HospitalInfomationService hospitalInfomationService, MainStore mainStore)
        {
            _mainStore = mainStore;
            _hospitalInfomationService = hospitalInfomationService;

            _hospitalInfo = _hospitalInfomationService.ReadHospitalInfomation();
            SyncrosizeHospitalUI();
        }

        public void OnWindowActivated()
        {
            _hospitalInfo = _hospitalInfomationService.ReadHospitalInfomation();
            SyncrosizeHospitalUI();
        }

        partial void OnNameChanged(string value)
        {
            _hospitalInfo.Name = value;
            _hospitalInfomationService.UpdateHospitalInfo(_hospitalInfo);
        }

        partial void OnIdChanged(string value)
        {
            _mainStore.Id = value;
            _hospitalInfo.Id = value;
            _hospitalInfomationService.UpdateHospitalInfo(_hospitalInfo);
        }

        partial void OnPasswordChanged(string value)
        {
            _mainStore.Password = value;
            _hospitalInfo.Password = value;
            _hospitalInfomationService.UpdateHospitalInfo(_hospitalInfo);
        }
        private void SyncrosizeHospitalUI()
        {
            Name = _hospitalInfo.Name;
            Id = _hospitalInfo.Id;
            Password = _hospitalInfo.Password;
        }
    }
}
