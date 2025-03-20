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
        private readonly ProcessLaunceService _processService;
        
        public ObservableCollection<ProgramItem> Programs { get; } = new();

        public IntroLauncherViewModel(ProcessLaunceService processService)
        {
            _processService = processService;

            // TODO: Change how to initialize Programs
            Programs.Add(new ProgramItem("메모장", @"C:\Windows\notepad.exe"));
            Programs.Add(new ProgramItem("계산기", @"C:\Windows\System32\calc.exe"));
            Programs.Add(new ProgramItem("명령 프롬프트", @"C:\Windows\System32\cmd.exe"));
        }

        [RelayCommand]
        public void StartProgram(ProgramItem item)
        {
            if (item.Path == null) return;

            _processService.StartProgram(item.Path);
        }

    }
}
