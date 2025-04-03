using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DashBoard.Models.IntroLauncher;
using DashBoard.Services;
using DashBoard.Services.IntroLauncher;

namespace DashBoard.ViewModels
{    
    public partial class IntroLauncherViewModel : ObservableObject, IViewModelBase
    {
        private readonly ProcessLauncherService _processService;
        private readonly HospitalInfomationService _hospitalInfomationService;

        // ProgramItem
        [ObservableProperty]
        private string _id = "";
        [ObservableProperty]
        private string _password = "";

        // Hospital Infomation        
        [ObservableProperty]
        private string fileContent;        

        public ObservableCollection<ProgramItem> Programs { get; } = new();

        public IntroLauncherViewModel(ProcessLauncherService processService, HospitalInfomationService hospitalInfomationService)
        {
            _processService = processService;
            _hospitalInfomationService = hospitalInfomationService;

            _processService.getProgramItemList()?.ForEach(Programs.Add);
            fileContent = _hospitalInfomationService.ReadHospitalInfomation();

            _hospitalInfomationService.HospitalInfoChanged += content =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    FileContent = content;
                });
            };
        }

        [RelayCommand]
        public void StartProgram(ProgramItem program)
        {
            if (program.Path == null) return;

            _processService.StartProgram(program, Id, Password);
        }
    }
}
