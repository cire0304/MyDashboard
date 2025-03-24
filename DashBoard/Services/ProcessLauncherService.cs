using System.Configuration;
using System.Diagnostics;
using System.IO;
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

        public void StartProgram(string programPath)
        {
            if (!File.Exists(programPath)) return;

            Process.Start(new ProcessStartInfo
            {
                FileName = programPath,
                UseShellExecute = true
            });
        }

        public List<ProgramItem>? getProgramItemList()
        {
            return _configuration
                .GetSection("LauncherProgramList")
                .Get<List<ProgramItem>>();
        }
    }
}
