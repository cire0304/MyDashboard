using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DashBoard.Models;
using DashBoard.Models.IntroLauncher;
using DashBoard.Services.IntroLauncher;
using DashBoard.Stores;

namespace DashBoard.ViewModels
{
    public partial class ProgramLancherViewModel : ObservableObject, IViewModelBase
    {
        private readonly ProcessLauncherService _processService;

        public ObservableCollection<ProgramItem> Programs { get; } = new();
        public ObservableCollection<CommandItem> Commands { get; } = new();
        public MainStore _mainStore;

        public ProgramLancherViewModel(ProcessLauncherService processService, MainStore mainStore)
        {
            _mainStore = mainStore;
            _processService = processService;
            _processService.getProgramItemList()?.ForEach(Programs.Add);
            _processService.getComandItemList()?.ForEach(Commands.Add);
        }

        [RelayCommand]
        public void StartProgram(ProgramItem program)
        {
            if (program.Path == null) return;

            _processService.StartProgram(program, _mainStore.Id, _mainStore.Password);
        }

        [RelayCommand]        
        public void LaunchProgramByIndex(int index)
        {
            if (index >= 0 && index < Programs.Count)
            {
                StartProgramCommand.Execute(Programs[index]);
            }
        }

        [RelayCommand]
        public void StartCommand(CommandItem program)
        {            
            _processService.StartCommand(program);
        }
    }
}
