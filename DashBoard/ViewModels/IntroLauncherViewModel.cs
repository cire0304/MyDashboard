using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DashBoard.Models.IntroLauncher;
using DashBoard.Services.IntroLauncher;

namespace DashBoard.ViewModels
{    
    public partial class IntroLauncherViewModel : ObservableObject, IViewModelBase
    {
        private readonly ProcessLauncherService _processService;

        [ObservableProperty]
        private string _id = "";
        [ObservableProperty]
        private string _password = "";


        public ObservableCollection<ProgramItem> Programs { get; } = new();

        public IntroLauncherViewModel(ProcessLauncherService processService)
        {
            _processService = processService;

            _processService.getProgramItemList()?.ForEach(Programs.Add);

        }

        [RelayCommand]
        public void StartProgram(ProgramItem program)
        {
            if (program.Path == null) return;

            _processService.StartProgram(program, Id, Password);
        }

    }
}
