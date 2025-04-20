using System.Configuration;
using System.Diagnostics;
using System.IO;
using DashBoard.Models;
using DashBoard.Models.IntroLauncher;
using Microsoft.Extensions.Configuration;

namespace DashBoard.Services.IntroLauncher
{
    public class ProcessLauncherService
    {
        private readonly IConfiguration _configuration;

        public ProcessLauncherService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void StartProgram(ProgramItem programPath, string id, string password)
        {
            if (!File.Exists(programPath.Path)) return;

            Process.Start(new ProcessStartInfo
            {
                FileName = programPath.Path,
                Arguments = programPath.getArgument(id, password),
                UseShellExecute = true
            });
        }

        public void StartCommand(CommandItem commandItem)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = commandItem.FileName,
                Arguments = commandItem.Arguments,
                UseShellExecute = true
            });
        }

        public List<ProgramItem>? getProgramItemList()
        {
            return _configuration
                .GetSection("LauncherProgramList")
                .Get<List<ProgramItem>>();
        }

        public List<CommandItem>? getComandItemList()
        {
            return _configuration
                .GetSection("CommandList")
                .Get<List<CommandItem>>();
        }
    }
}
