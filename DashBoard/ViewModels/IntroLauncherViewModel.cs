using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DashBoard.Models.IntroLauncher;
using DashBoard.Services.IntroLauncher;

namespace DashBoard.ViewModels
{
    //[ObservableObject]
    public partial class IntroLauncherViewModel : ViewModelBase
    {
        private readonly ProcessLauncherService _processService;
        
        public ObservableCollection<ProgramItem> Programs { get; } = new();

        public IntroLauncherViewModel(ProcessLauncherService processService)
        {
            _processService = processService;

            _processService.getProgramItemList()?.ForEach(Programs.Add);

        }

        [RelayCommand]
        public void StartProgram(ProgramItem item)
        {
            if (item.Path == null) return;

            _processService.StartProgram(item.Path);
        }

    }
}
